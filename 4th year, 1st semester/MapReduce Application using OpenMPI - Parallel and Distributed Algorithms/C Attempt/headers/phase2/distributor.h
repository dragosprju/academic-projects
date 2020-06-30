#ifndef distributer_h
#define distributer_h

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>

#include "../common_tools.h"

#include "mpi.h"

inline int get_fileindex_from_node_rank(FILES* files, int rank) {
	int filei;
	for (filei = 0; filei < files->no_files; filei++) {
		if (files->file_destinations[filei] == rank) {
			return filei;
		}
	}
	return -1;
}

inline int get_next_free_node(NODES* nodes) {
	int nodei, searching = 1;

	nodei = nodes->global_node_index;
	while (searching) {
		do {
			nodei++;

			if (nodei > nodes->no_nodes) {
				nodei = 0;
			}
		} while (nodei == nodes->root);

		if (nodes->workload_amount[nodei] <= 0) {
			nodes->global_node_index = nodei;
			return nodei;
		}

		if (nodei == nodes->global_node_index) {
			searching = 0;
		}
	}

	return -1;
}

inline void mark_node_free(int node, NODES* nodes) {
	nodes->workload_amount[node] = 0;
}

inline void mark_node_busy(int workload_amount, int node, NODES* nodes) {
	nodes->workload_amount[node] = workload_amount;
}

inline void report_start_sending(int status, int node, int extra_prints) {
	if (status == 0 && extra_prints) {
		printf("[" KBLD KBLU "ROOT" KNRM "] Sent start index minifile to node "
				KBLD KWHT "%d" KNRM ".\r\n", node);
	}
	else if (status != 0) {
		fprintf(stderr, "[" KBLD KBLU "ROOT" KNRM "] " KRED "Couldn't send minifile start index to node "
				KBLD KWHT "%d" KNRM KRED ". Exiting." KNRM "\r\n", node);
		exit(-1);
	}
}

inline void report_end_sending(int status, int node, int extra_prints) {
	if (status == 0 && extra_prints) {
		printf("[" KBLD KBLU "ROOT" KNRM "] Sent end index minifile to node "
				KBLD KWHT "%d" KNRM ".\r\n", node);
	}
	else if (status != 0) {
		fprintf(stderr, "[" KBLD KBLU "ROOT" KNRM "] " KRED "Couldn't send minifile end index to node "
				KBLD KWHT "%d" KNRM KRED ". Exiting." KNRM "\r\n", node);
		exit(-1);
	}
}

inline void report_bad_free_node(int free_node, NODES* nodes, int extra_prints) {
	if (free_node != -1 && extra_prints) {
		printf("[" KBLD KBLU "ROOT" KNRM "] Chosen free node "
				KBLD KWHT "%d" KNRM ".\r\n", free_node);
	}
	else if (free_node == -1) {
		fprintf(stderr, "[" KBLD KBLU "ROOT" KNRM "] " KRED "Couldn't find a free node. Exiting." KNRM "\r\n");
		fprintf(stderr, "Dumping workload amount vector before that: ");
		int i = 0;
		for (i = 0; i < nodes->no_nodes; i++) {
			fprintf(stderr, "%d ", nodes->workload_amount[i]);
		}
		fprintf(stderr, "\r\n");
		exit(-1);
	}
}

int receive_minifiles_no_from_any_node(int* node_received_from, int timeout, int extra_prints);
int get_next_free_node_with_timeout(NODES* nodes, int timeout);
void tell_free_node_to_map_next_n_minifiles(int start, int end, NODES* nodes, int timeout_sending, int timeout_occupancy, int extra_prints);


#endif
