#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <dirent.h>
#include "init_check_alloc_apply/checkers.h"
#include "files.h"

char* construct_string_from_three(char* string1, char* string2, char* string3) {
	char* new_string = NULL;
	int new_string_len;

	new_string_len = strlen(string1) + strlen(string2) + strlen(string3) + 1;
	new_string = (char*) safe_malloc(sizeof(char) * new_string_len, new_string, "new_string (in construct_string_from_three)");
	
	strcpy(new_string, string1);
	strcat(new_string, string2);
	strcat(new_string, string3);
	new_string[new_string_len] = '\0';

	return new_string;
}

void make_filepaths_list(FILES* files, int extra_prints) {

	char* dirpath = files->dirpath;

	/* Presupunem că verificarea directoriului în sine 
 	 * (încât să nu eșueze opendir și closedir) s-a făcut deja */
	DIR* dir = opendir(dirpath);

	/* Vom citi fiecare fișier în parte dacă poate fi citit */
	FILE* file = NULL;
	struct dirent* dirfile;

	int filei = 0;
	while((dirfile = readdir(dir))) {
		/* Evităm procesarea '.' și '..' */
		if(is_it_dots(dirfile)) continue;

		/* Construim calea absolute a fișierului ce trebuie indexat */
		files->filepaths[filei] = construct_string_from_three(dirpath, "/", dirfile->d_name);

		/* Verificăm dacă fișierul se deschide și dacă nu, în
		 * 'file_destinations' punem valorea -2 pt. a omite pe viitor fișierul */
		file = fopen(files->filepaths[filei], "r");		

		if (file == NULL) {
			char* filename = get_last_token(files->filepaths[filei], '/');
			fprintf(stderr, KYEL "File '" KBLD KWHT "%s" KNRM KYEL "' is unreadable.\r\n" KNRM, filename);
			safe_free(filename, "filename (1st one in make_filepaths_list)");

			files->file_destinations[filei] = -2;
		}
		else {
			/* Nu fă nimic.
			 * 'files->file_destinations[filei] == -1' înseamnă că fișierul e bun
			 * dar încă nu are asignat destinatar. */
			if (extra_prints) {
				char* filename = get_last_token(files->filepaths[filei], '/');
				fprintf(stderr, "File '" KBLD KWHT "%s" KNRM "' is readable.\r\n" KNRM, filename);
				safe_free(filename, "filename (2nd one in make_filepaths_list)");
			}
		}
		filei++;
	}
}

void delete_unreadable_filepaths(FILES* files, int extra_prints) {
	int filei, act_filei;

	int defected_files = 0;
	for (filei = 0; filei < files->no_files; filei++) {
		if (files->file_destinations[filei] == -2) {			
			if (extra_prints) {
				char* filename = get_last_token(files->filepaths[filei], '/');
				fprintf(stderr, KYEL "File '" KBLD KWHT "%s" KNRM KYEL "' removed from rotation.\r\n" KNRM, filename);
				safe_free(filename, "filename (1st one in delete_unreadable_filepaths)");
			}

			defected_files++;
			safe_free(files->filepaths[filei], "files->filepaths[filei]");
			files->filepaths[filei] = NULL;
		}
	}

	int actual_no_files = files->no_files - defected_files;
	char** aux_filepaths = NULL;
	aux_filepaths = (char**) safe_malloc(sizeof(char*) * actual_no_files, aux_filepaths, "aux_filepaths");
	
	filei = -1;
	for (act_filei = 0; act_filei < actual_no_files; act_filei++) {
		filei = (files->file_destinations[filei + 1] == -2) ? (filei + 2) : (filei + 1);
		
		if (extra_prints) {
			char* filename = get_last_token(files->filepaths[filei], '/');
			fprintf(stderr, "Adding file '" KBLD KWHT "%s" KNRM "' to rotation.\r\n", filename);
			safe_free(filename, "filename (2nd one in delete_unreadable_filepaths)");
		}

		aux_filepaths[act_filei] = NULL;
		aux_filepaths[act_filei] = (char*) safe_malloc(sizeof(char*) * (strlen(files->filepaths[filei]) + 1), aux_filepaths[act_filei], "aux_filepaths[act_filei] (from delete_unreadable_filepaths)");
		strcpy(aux_filepaths[act_filei], files->filepaths[filei]);

		safe_free(files->filepaths[filei], "files->filepaths[filei] (from delete_unreadable_filepaths)");
	}

	safe_free(files->filepaths, "files->filepaths (in delete_unreadable_filepaths)");

	files->no_files = actual_no_files;
	files->filepaths = aux_filepaths;	
}

extern inline int select_next_node(int curr_node, int root, int no_nodes);

void round_robin_files(FILES* files, NODES* nodes, int extra_prints) {
	int filei, nodei;

	nodei = -1; 
	for (filei = 0; filei < files->no_files; filei++) {
		nodei = select_next_node(nodei, nodes->root, nodes->no_nodes);
		files->file_destinations[filei] = nodei;
		nodes->workload_amount[nodei]++;

		if (extra_prints) {
			char* filename = get_last_token(files->filepaths[filei], '/');
			fprintf(stderr, "File '" KBLD KWHT "%s" KNRM " assigned to node %d.\r\n", filename, nodei);
			safe_free(filename, "filename (from round_robin_files)");
		}
	}
}


