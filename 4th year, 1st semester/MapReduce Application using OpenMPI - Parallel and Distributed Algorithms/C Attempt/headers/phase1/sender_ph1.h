#ifndef sender_ph1_h
#define sender_ph1_h

#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include "mpi.h"

#include "../defines.h"
#include "../common_tools.h"

inline int wait_for_send_response(MPI_Request* req, int timeout) {
	int finished, secs;
	for (secs = 0; secs < timeout * 10; secs++) {
		MPI_Test(req, &finished, MPI_STATUS_IGNORE);
		if (finished != 0) {
			break;
		}
		sleep_ms(100);
	}

	if (finished != 0) {
		return 0;
	}
	else {
		return -1;
	}
}

inline int send_with_timeout(const void* what, int how_many, MPI_Datatype what_type, int where, int timeout) {
	MPI_Request req;
	int status;

	MPI_Issend(what, how_many, what_type, where, TAG, MPI_COMM_WORLD, &req);
	status = wait_for_send_response(&req, timeout);

	return status;
}

//void send_command_to_nodes(COMMAND cmd, NODES* nodes, int timeout, int extra_prints);
void send_msg_counts_to_nodes(FILES* files, NODES* nodes, int timeout, int extra_prints);
void send_filepaths_for_each_node(FILES* files, NODES* nodes, int timeout, int extra_prints);
#endif
