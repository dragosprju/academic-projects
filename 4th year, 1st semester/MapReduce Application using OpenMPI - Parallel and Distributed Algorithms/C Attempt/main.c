#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include "mpi.h"

#include "headers/typedefs.h"
#include "headers/defines.h"
#include "headers/common_tools.h"
#include "headers/phase0/init_check_alloc_apply/initializers.h"
#include "headers/phase0/init_check_alloc_apply/checkers.h"
#include "headers/phase0/init_check_alloc_apply/appliers.h"
#include "headers/phase0/init_check_alloc_apply/allocators.h"
#include "headers/phase0/files.h"

#include "headers/phase1/sender_ph1.h"
#include "headers/phase1/receiver_ph1.h"

#include "headers/phase2/receiver_ph2.h"
#include "headers/phase2/divider.h"
#include "headers/phase2/distributor.h"

#include "headers/phase3/receiver_ph3.h"

#include "headers/phasez/free-ers.h"

int main (int argc, char **argv) {
	int no_nodes, rank;
	int no_files;

	NODES* nodes = NULL;
	FILES* files = NULL;

	//COMMAND cmd;
	int workload_amount;
	char** filepaths = NULL;

	MPI_Init(&argc, &argv);
	MPI_Comm_size(MPI_COMM_WORLD, &no_nodes);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	/* Faza 0: Rădăcina pregătește informațiile de început pt. a împarte 
         * la nodurile copil */
	if (rank == ROOT) {
		/* Inițializăm structurile principale
		   * nodes, care plimbă rădăcina și nr. de noduri
                   * files, care plimbă 
		     * nr. de fișiere
                     * calea spre directoriu
                     * căile absolute spre fișiere
		     * fiecare fișier la ce nod este asignat pentru prelucrare */
		nodes = initialize_nodes(nodes);
		files = initialize_files(files);

		/* 1. Verific dacă este cel puțin un argument.
		 * 2. Verific dacă -np a fost setat cu mai mult de un nod.
		 * 3. Verific dacă directoriul poate fi deschis spre citire.
		 * 4. Verific dacă există fișiere în directoriu. 
		      * Numărăm și fișierele dacă tot trecem prin ele */
		check_if_theres_an_argument(argc);
		check_num_processes(no_nodes);
		check_directory_can_be_read(argv[1], EXTRA_PRINTS_PHASE_0_1);
		no_files = check_files_exist(argv[1], EXTRA_PRINTS_PHASE_0_1);

		/* 1. Atribui în 'nodes->root' ROOT-ul.
		   2. Atribui în 'nodes->no_nodes' nr. de noduri.
		   3. Atribui în 'files->no_files' numărul de fișiere citite.
		   4. Atribui în 'files->dirpath' adresa absolută string a
			directoriului din care citesc.
		   5. Aloc 'files->filepaths' de dimensiunea câte fișiere sunt și
			atribui NULL-uri.
		   6. Aloc 'files->file_destinations' de dimensiunea câte fișiere
			sunt și atribui -1. */
		nodes = apply_nodes_values(ROOT, no_nodes, nodes);
		nodes = allocate_workload_amounts(nodes);

		files = allocate_dirpath(files, argv[1]);
		files = apply_files_values(no_files, argv[1], files);

		files = allocate_filepaths(files);
		files = allocate_file_destinations(files);

		/* Citesc din directoriul ales și extrag adresa absolută a fiecărui
		 * fișier, ca să le pun în vectorul de stringuri 'files->filepaths' */
		make_filepaths_list(files, EXTRA_PRINTS_PHASE_0_2);
		delete_unreadable_filepaths(files, EXTRA_PRINTS_PHASE_0_3);

		/* Împart care fișier unde se duce și salvez în 'files->file_destinations' */
		round_robin_files(files, nodes, EXTRA_PRINTS_PHASE_0_4);	

		/**************************
		 * Sunt gata să transmit. *
		 **************************/	
	}
	else {
 		/* Verific dacă rădăcina e mai mică decât -np-ul setat.
		 * Trebuie verificat de toate nodurile copil pentru că rădăcina
		 * dacă e mai mare decât -np nici n-ar intra în if-ul de sus */
		check_root_validity(ROOT, no_nodes);

		/*************************
		 * Sunt gata să primesc. *
		 *************************/	
	}

	/* Faza 1: 
	 * a. Rădăcina trimite fișierele de prelucrat la noduri.
     * b. Nodurile:
     *    1. primesc comanda de a aștepta adresele fișierelor de lucru
     *    2. primesc numărul de fișiere pe care să se aștepte să primească
     *    3. primesc câte o adresă de fișier de atâtea ori câte fișiere li s-au spus
	 *     că vor primi (MPI_Irecv de atâtea ori). */
	if (rank == ROOT) {
		send_msg_counts_to_nodes(files, nodes, TIMEOUT_SENDER, EXTRA_PRINTS_PHASE_1_1);
		send_filepaths_for_each_node(files, nodes, TIMEOUT_SENDER, EXTRA_PRINTS_PHASE_1_3);
	}
	else {
		workload_amount = receive_msg_counts(ROOT, rank, TIMEOUT_RECEIVER, EXTRA_PRINTS_PHASE_1_2);
		filepaths = receive_filepaths(ROOT, rank, workload_amount, TIMEOUT_RECEIVER, EXTRA_PRINTS_PHASE_1_4);	
	}

	/* Faza 2: Divizare
	 *	a. Rădăcina așteaptă numărătoarea divizării fișierelor servite nodurilor copil și va asigna
	 *		live (în timp ce primește) taskul de a mapa conținutul fișierelor divizate în
	 *		format <fișier, cuvânt, 1> fiecărui nod care răspunde sau care e deja liber.
	 *	b. Nodurile: Cum unele fișiere sunt mai mari decât altele,
     *  	va încerca să dividă fișierele primite în fișiere de număr de CARACTERE egale.
     *		Bineînțeles, va încerca să 'decupeze' la sfârșitul unui cuvânt și nu în mijlocul lui
     *		când ajunge la limita caracterelor pentru acel 'minifișier' obținut din divizare.
     *		Între timp, se elimină și caracterele irelevante și dubluri de spațiu. */
	if (rank == ROOT) {
		int node_received_from, files_left;
		int minifiles_left = 0;
		int minifile_index = 0;
		int should_map_a_no_of;
		int who_to_free;

		for (files_left = files->no_files; files_left > 0; files_left--) {
			minifiles_left += receive_minifiles_no_from_any_node(&node_received_from, TIMEOUT_RECEIVER, EXTRA_PRINTS_PHASE_2_2);

			/* Vrem o valoare intermediară și nu maximă a numărului de fișiere
			 * de dat spre mapare pentru fiecare nod, încât dacă toate nodurile
			 * s-ar elibera cascadat și în ordine, să se împartă egal fișierele
			 * la fiecare fără a ști numărul total de fișiere în prealabil (și
			 * a face un blocaj până rădăcina ar afla acest lucru).
			 * Când se primește nr. de minifișiere de la PENULTIMUL nod, atunci
			 * am constatat a fi o valoare prielnică pentru acest lucru. */
			if (files_left != 1) {
				should_map_a_no_of = minifiles_left / nodes->no_nodes;
			}

			mark_node_free(node_received_from, nodes);
			tell_free_node_to_map_next_n_minifiles(minifile_index, minifile_index + should_map_a_no_of, nodes, TIMEOUT_SENDER, TIMEOUT_OCCUPANCY, EXTRA_PRINTS_PHASE_2_3);
			minifile_index += should_map_a_no_of;
			minifiles_left -= should_map_a_no_of;
		}

		// N-ar trebui dar în caz de...
		if (should_map_a_no_of == 0) {
			should_map_a_no_of = 1;
		}

		while (minifiles_left) {
			tell_free_node_to_map_next_n_minifiles(minifile_index, minifile_index + should_map_a_no_of, nodes, TIMEOUT_SENDER, TIMEOUT_OCCUPANCY, EXTRA_PRINTS_PHASE_2_3);
			minifile_index += should_map_a_no_of;
			minifiles_left -= should_map_a_no_of;

			who_to_free = probe_msg_source(TIMEOUT_OCCUPANCY);
			if (who_to_free != -1) {
				receive_int_without_timeout(1, who_to_free);
				mark_node_free(who_to_free, nodes);
			}
			else {
				;
				/* Doar treci peste. Poate încă mai sunt noduri libere la care să se dea de lucru
				 * și de fapt se stă secunde bune după cel care ar trebui să termine
				 * numai ca să nu termine în timp, să tratăm -1 ca eroare fatală și să închidem totul
				 * fără motiv de fapt. */
			}

		}

		int nodei;
		int signal_to_stop = -1;
		for (nodei = 0; nodei < nodes->no_nodes; nodei++) {
			if (nodes->workload_amount[nodei] == 0) {
				send_with_timeout(&signal_to_stop, 1, MPI_INT, nodei, TIMEOUT_SENDER);
				send_with_timeout(&signal_to_stop, 1, MPI_INT, nodei, TIMEOUT_SENDER);
			}
		}
	}
	else {
		int filei, minifiles_no, status;

		report_nothing_to_divide(workload_amount, rank, EXTRA_PRINTS_PHASE_2_1);

		for (filei = 0; filei < workload_amount; filei++) {
			minifiles_no = divide_file_into_minifiles(filepaths[filei], rank, OF_N_CHARS);
			status = send_with_timeout(&minifiles_no, 1, MPI_INT, ROOT, TIMEOUT_SENDER);
			report_minifile_no_sending(status, ROOT, rank, filepaths[filei], minifiles_no, EXTRA_PRINTS_PHASE_2_1);
		}
	}

	/* Faza 3: Indexare */
	if (rank == ROOT) {
		;
	}
	else {
		int mf_start, mf_end;
		int still_coming = 1;
		int any_value = 0;

		while (still_coming) {
			still_coming = receive_start_end_of_minifiles_to_map(&mf_start, &mf_end, rank, ROOT, TIMEOUT_RECEIVER, EXTRA_PRINTS_PHASE_3_1);

			send_with_timeout(&any_value, 1, MPI_INT, ROOT, TIMEOUT_SENDER);
		}
	}

	/* Faza 4: Inversare */

	/* Faza 5: Indexare inversă */

	/* Faza Z: Aici se vor șterge cât mai mulți pointeri neuitați */
	if (rank == ROOT) {
		free_nodes(nodes);
		free_files(files);
	}
	else {
		free_filepaths(filepaths, workload_amount);
	}

	check_all_pointers_deallocated(rank, FORGOTTEN_POINTERS_PRINT);

	MPI_Finalize();
}
	
		
