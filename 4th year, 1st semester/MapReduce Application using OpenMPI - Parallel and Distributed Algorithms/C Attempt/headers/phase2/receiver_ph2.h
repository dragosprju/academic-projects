#ifndef receiver2_h
#define receiver2_h

#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <time.h>
#include "mpi.h"

#include "../defines.h"
#include "../common_tools.h"
#include "mpi.h"

static inline int probe_msg_source(int timeout) {
	MPI_Status status;
	int finished = 0, secs;
	int detected_source;

	for (secs = 0; secs < timeout * 10; secs++) {
		MPI_Iprobe(MPI_ANY_SOURCE, TAG, MPI_COMM_WORLD, &finished, &status);
		if (finished != 0) {
			break;
		}
		sleep_ms(100);
	}

	if (finished != 0) {
		detected_source = status.MPI_SOURCE;
		return detected_source;
	}
	else {
		return -1;
	}
}

static inline int receive_int_without_timeout(int from_who, int init) {
	int where_to_put = init;

	MPI_Recv(&where_to_put, 1, MPI_INT, from_who, TAG, MPI_COMM_WORLD, MPI_STATUS_IGNORE);

	return where_to_put;
}

#endif
