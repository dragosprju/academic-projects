#ifndef receiver_ph1_h
#define receiver_ph1_h

#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <time.h>
#include "mpi.h"

#include "../defines.h"
#include "../common_tools.h"

inline int wait_for_recv_response(MPI_Request* req, int timeout) {
	int finished = 0, secs;
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

inline int probe_msg_len(int from_who, int timeout) {
	MPI_Status status;
	int finished = 0, secs;
	int msg_len;

	for (secs = 0; secs < timeout * 10; secs++) {
		MPI_Iprobe(from_who, TAG, MPI_COMM_WORLD, &finished, &status);
		if (finished != 0) {
			break;
		}
		sleep_ms(100);
	}

	if (finished != 0) {
		MPI_Get_count(&status, MPI_CHAR, &msg_len);
		return msg_len;
	}
	else {
		return -1;
	}	
}

inline int* receive_int_with_timeout(int how_many, int from_who, int timeout) {	
	MPI_Request req;
	int status;

	int* where_to_put = NULL;
	where_to_put = (int*) safe_malloc(sizeof(int) * how_many, where_to_put, "where_to_put (in receive_with_timeout)");

	MPI_Irecv(where_to_put, how_many, MPI_INT, from_who, TAG, MPI_COMM_WORLD, &req);
	status = wait_for_recv_response(&req, timeout);

	if (status != 0) {
		safe_free(where_to_put, "where_to_put (in receive_int_with_timeout because of receive failure)");
		return NULL;
	}
	else {
		return where_to_put;
	}
}

inline char* receive_chars_without_timeout(int how_many, int from_who) {
	char* where_to_put = NULL;
	where_to_put = (char*) safe_malloc(sizeof(char) * how_many, where_to_put, "where_to_put (in receive_chars_with_timeout)");
	
	MPI_Recv(where_to_put, how_many, MPI_CHAR, from_who, TAG, MPI_COMM_WORLD, MPI_STATUS_IGNORE);

	return where_to_put;
}

COMMAND receive_command(int root, int rank, int timeout, int extra_prints);
int receive_msg_counts(int root, int rank, int timeout, int extra_prints);
char** receive_filepaths(int root, int rank, int workload_amount, int timeout, int extra_prints);


#endif
