#include "../phase1/receiver_ph1.h"
#include "receiver_ph3.h"

int receive_start_end_of_minifiles_to_map(int* mf_start, int* mf_end, int rank, int root, int timeout, int extra_prints) {

	mf_start = receive_int_with_timeout(1, root, timeout);
	if (mf_start != NULL && extra_prints) {
		printf("[" KBLD KGRN "NODE%2d" KNRM "] Successfully received minifile start index "
			KBLD KWHT "%d" KNRM ". \r\n" KNRM, rank, *mf_start);
	}
	else if (mf_start == NULL) {
		fprintf(stderr, "[" KBLD KGRN "NODE%2d" KNRM "] " KBLD KRED "ERROR" KNRM KRED ": Failed to receive minifile start index from root. Exiting.\r\n", rank);
		exit(-1);
	}

	mf_end = receive_int_with_timeout(1, root, timeout);
	if (mf_end != NULL && extra_prints) {
		printf("[" KBLD KGRN "NODE%2d" KNRM "] Successfully received minifile end index "
			KBLD KWHT "%d" KNRM ". \r\n" KNRM, rank, *mf_end);
		printf("Yaaay!");
	}
	else if (mf_end == NULL) {
		fprintf(stderr, "[" KBLD KGRN "NODE%2d" KNRM "] " KBLD KRED "ERROR" KNRM KRED ": Failed to receive minifile end index from root. Exiting.\r\n", rank);
		exit(-1);
	}

	if (*mf_start == -1 || *mf_end == -1) {
		return 0;
	}
	else {
		return 1;
	}
}
