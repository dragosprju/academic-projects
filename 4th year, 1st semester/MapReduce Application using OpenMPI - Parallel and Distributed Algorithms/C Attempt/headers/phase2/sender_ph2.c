#include <string.h>

#include "../common_tools.h"
#include "../typedefs.h"
#include "../defines.h"

#include "../phase1/sender_ph1.h"
#include "sender_ph2.h"

void send_minifile_count_to_root(int root, int minifile_count, int rank, int timeout, int extra_prints) {
	int status;

	status = send_with_timeout(&minifile_count, 1, MPI_INT, root, timeout);

	if (status == 0 && extra_prints) {
		printf("[" KBLD KGRN "NODE%2d" KNRM "] Successfully sent minifile count '" KBLD KWHT "%d" KNRM "' to root " KBLD KBLU "%d" KNRM ".\r\n", rank, minifile_count, root);
	}
	else if (status != 0) {
		fprintf(stderr, "[" KBLD KGRN "NODE%2d" KNRM "] " KBLD KRED "ERROR" KNRM KRED ": Root '" KBLD KWHT "%d" KNRM KRED "' is not responding for minifile count sending. Exiting.\r\n" KNRM, rank, root);
		exit(-1);
	}
}
