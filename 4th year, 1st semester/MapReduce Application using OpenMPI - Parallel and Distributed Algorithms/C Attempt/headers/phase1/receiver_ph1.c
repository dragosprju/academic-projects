#include "../defines.h"
#include "../typedefs.h"
#include "receiver_ph1.h"

extern inline int wait_for_recv_response(MPI_Request* req, int timeout);
extern inline int probe_msg_len(int from_who, int timeout);
extern inline int* receive_int_with_timeout(int how_many, int from_who, int timeout);
extern inline char* receive_chars_without_timeout(int how_many, int from_who);

/*
COMMAND receive_command(int root, int rank, int timeout, int extra_prints) {
	COMMAND cmd;

	int* placeholder = receive_int_with_timeout(1, root, timeout);

	if (placeholder != NULL && extra_prints) {
		cmd = (*placeholder);
		printf("[" KBLD KGRN "NODE%2d" KNRM "] Successfully received command '" KBLD KWHT "%d" KNRM "'.\r\n" KNRM, rank, cmd);
	}
	else if (placeholder == NULL) {
		fprintf(stderr, "[" KBLD KGRN "NODE%2d" KNRM "] " KBLD KRED "ERROR" KNRM KRED ": Failed to receive command from root "
			KBLD KWHT "%d" KNRM KRED". Exiting" KNRM ".\r\n", rank, root);
		exit(-1);
	}

	safe_free(placeholder, "placeholder (in receive_command)");

	return cmd;
}
*/

int receive_msg_counts(int root, int rank, int timeout, int extra_prints) {
	int* wrkld_amnt_ptr;
	int workload_amount;

	wrkld_amnt_ptr = receive_int_with_timeout(1, root, timeout);
	if (wrkld_amnt_ptr != NULL && extra_prints) {
		printf("[" KBLD KGRN "NODE%2d" KNRM "] Successfully received workload amount '"
			KBLD KWHT "%d" KNRM "'. \r\n" KNRM, rank, *wrkld_amnt_ptr);
	}
	else if (wrkld_amnt_ptr == NULL) {
		fprintf(stderr, "[" KBLD KGRN "NODE%2d" KNRM "] " KBLD KRED "ERROR" KNRM KRED ": Failed to receive workload amount from root " 
			KBLD KWHT "%d" KNRM KRED". Exiting.\r\n", rank, root);
		exit(-1);
	}

	workload_amount = *wrkld_amnt_ptr;
	safe_free(wrkld_amnt_ptr, "wrkld_amnt_ptr (in receive_command)");

	return workload_amount;
}

char** receive_filepaths(int root, int rank, int workload_amount, int timeout, int extra_prints) {
	char** filepaths = NULL;
	int filei, no_chars;

	filepaths = (char**) safe_malloc(sizeof(char*) * workload_amount, filepaths, "filepaths (in receive_filepaths)");

	for (filei = 0; filei < workload_amount; filei++) {
		filepaths[filei] = NULL;

		no_chars = probe_msg_len(root, timeout);

		if (no_chars > 0) {
			filepaths[filei] = receive_chars_without_timeout(no_chars, root);
		}

		if (filepaths[filei] != NULL && extra_prints) {
			char* filename = get_last_token(filepaths[filei], '/');
			printf("[" KBLD KGRN "NODE%2d" KNRM "] Successfully received filepath for '" KBLD KWHT "%s" KNRM "'. \r\n" KNRM, rank, filename);
			safe_free(filename, "filename (in receive_filepaths)");
		}
		else if(filepaths[filei] == NULL) {
			fprintf(stderr, "[" KBLD KGRN "NODE%2d" KNRM "] " KBLD KRED "ERROR" KNRM KRED ": Failed to receive filepath amount from root "
			 KBLD KWHT "%d" KNRM KRED". Exiting." KNRM "\r\n", rank, root);
			exit(-1);
		}
	}

	return filepaths;
}
