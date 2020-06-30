#include <string.h>

#include "../common_tools.h"
#include "../typedefs.h"
#include "../defines.h"
#include "sender_ph1.h"

extern inline int wait_for_send_response(MPI_Request* req, int timeout);
extern inline int send_with_timeout(const void* what, int how_many, MPI_Datatype what_type, int where, int timeout);

/*
void send_command_to_nodes(COMMAND cmd, NODES* nodes, int timeout, int extra_prints) {
	int nodei, status;
	for (nodei = 0; nodei < nodes->no_nodes; nodei++) {
		if (nodei == nodes->root) continue;
		status = send_with_timeout(&cmd, 1, MPI_INT, nodei, timeout);

		if (status == 0 && extra_prints) {
			printf("[" KBLD KBLU "ROOT" KNRM "] Successfully sent command '" KBLD KWHT "%d" KNRM "' to node " KBLD KBLU "%d" KNRM ".\r\n", cmd, nodei);
		}
		else if (status != 0) {
			fprintf(stderr, "[" KBLD KBLU "ROOT" KNRM "] " KBLD KYEL "WARNING" KNRM KYEL ": Node '" KBLD KWHT "%d" KNRM KYEL "' is not responding for command sending. Skipping but losing workload.\r\n" KNRM, nodei);
		}
	}	
}
*/

void send_msg_counts_to_nodes(FILES* files, NODES* nodes, int timeout, int extra_prints) {
	int nodei, status;
	for (nodei = 0; nodei < nodes->no_nodes; nodei++) {
		if (nodei == nodes->root) continue;
		status = send_with_timeout(&nodes->workload_amount[nodei], 1, MPI_INT, nodei, timeout);
		if (status == 0 && extra_prints) {
			printf("[" KBLD KBLU "ROOT" KNRM "] Successfully sent workload amount '" KBLD KWHT "%d" KNRM "' to node " KBLD KBLU "%d" KNRM ".\r\n", nodes->workload_amount[nodei], nodei);
		}
		else if (status != 0) {
			fprintf(stderr, "[" KBLD KBLU "ROOT" KNRM "] " KBLD KYEL "WARNING" KNRM KYEL ": Node '" KBLD KWHT "%d" KNRM KYEL "' is not responding for workload amount sending. Skipping but losing workload.\r\n" KNRM, nodei);
		}
	}
}

void send_filepaths_for_each_node(FILES* files, NODES* nodes, int timeout, int extra_prints) {
	int filei, status;
	for (filei = 0; filei < files->no_files; filei++) {
		char* filepath = files->filepaths[filei];
		int filepath_len = strlen(files->filepaths[filei]) + 1;
		int destination = files->file_destinations[filei];
		status = send_with_timeout(filepath, filepath_len, MPI_CHAR, destination, timeout);

		if (status == 0 && extra_prints) {
			char* filename = get_last_token(filepath, '/');
			printf("[" KBLD KBLU "ROOT" KNRM "] Successfully sent filepath '" KBLD KWHT "%s" KNRM "' to node " KBLD KBLU "%d" KNRM ".\r\n", filename, destination);
			safe_free(filename, "filename (in send_filepaths_for_each_node on sending success)");
		}
		else if (status != 0) {
			fprintf(stderr, KYEL "[" KBLD KBLU "ROOT" KNRM "] " KBLD KYEL "ERROR" KNRM KYEL ": Node '" KBLD KWHT "%d" KNRM KYEL "' is not responding for filepath sending. Skipping but losing workload." KNRM "\r\n", destination);
		}
	}
}
