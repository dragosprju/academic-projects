#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

#include "../common_tools.h"
#include "../defines.h"
#include "../phase1/sender_ph1.h"
#include "receiver_ph2.h"
#include "distributor.h"

extern inline int get_fileindex_from_node_rank(FILES* files, int rank);
extern inline void mark_node_free(int node, NODES* nodes);
extern inline void mark_node_busy(int workload_amount, int node, NODES* nodes);
extern inline int get_next_free_node(NODES* nodes);

extern inline void report_bad_free_node(int free_node, NODES* nodes, int extra_prints);
extern inline void report_start_sending(int status, int node, int extra_prints);
extern inline void report_end_sending(int status, int node, int extra_prints);

int receive_minifiles_no_from_any_node(int* node_received_from, int timeout, int extra_prints) {
	int source, minifiles_no = -1;

	source = probe_msg_source(timeout);
	if (source >= 0) {
		minifiles_no = receive_int_without_timeout(source, -1);
	}

	if (minifiles_no > 0 && extra_prints) {
		// Received correctly
		printf("[" KBLD KBLU "ROOT" KNRM "] Successfully received account of " KBLD KWHT "%d" KNRM " minifiles created by node " KBLD KBLU "%d" KNRM ".\r\n", minifiles_no, source);
	}
	else if (minifiles_no == 0) {
		// Weirdly received 0 minifiles created
		fprintf(stderr, "[" KBLD KBLU "ROOT" KNRM "]" KBLD KYEL "WARNING" KNRM KYEL ": Notified that " KBLD KWHT "no" KNRM " minifiles were created by node " KBLD KGRN "%d" KNRM KYEL ".\r\n" KNRM, source);
	}
	else if (minifiles_no < 0){
		// Error
		fprintf(stderr, "[" KBLD KBLU "ROOT" KNRM "]" KBLD KRED " ERROR" KNRM KRED ": Failed to receive minifiles count from node " KBLD KWHT "%d" KNRM KRED " Exiting.\r\n" KNRM, source);
		exit(-1);
	}

	(*node_received_from) = source;
	return minifiles_no;
}

int get_next_free_node_with_timeout(NODES* nodes, int timeout) {
	int secs;
	int free_node = -1;

	for (secs = 0; secs < timeout * 10; secs++) {
		free_node = get_next_free_node(nodes);
		if (free_node != -1) {
			break;
		}
		sleep_ms(100);
	}

	return free_node;
}

void tell_free_node_to_map_next_n_minifiles(int start, int end, NODES* nodes, int timeout_sending, int timeout_occupancy, int extra_prints) {
	int status, workload_amount;
	int free_node = -1;

	free_node = get_next_free_node_with_timeout(nodes, timeout_occupancy);
	report_bad_free_node(free_node, nodes, extra_prints);

	status = send_with_timeout(&start, 1, MPI_INT, free_node, timeout_sending);
	report_start_sending(status, free_node, extra_prints);

	status = send_with_timeout(&end, 1, MPI_INT, free_node, timeout_sending);
	report_end_sending(status, free_node, extra_prints);

	workload_amount = end - start;
	mark_node_busy(workload_amount, free_node, nodes);
}
