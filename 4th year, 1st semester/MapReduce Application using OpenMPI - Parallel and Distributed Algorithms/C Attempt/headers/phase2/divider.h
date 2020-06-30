#ifndef divider_h
#define divider_h

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>

#include "../common_tools.h"
#include "../phase1/sender_ph1.h"

#include "mpi.h"

inline FILE* safe_fopen(char* filepath, char* modes, int rank) {
	FILE* file_to_open = NULL;

	file_to_open = fopen(filepath, modes);
	if (file_to_open == NULL) {
		switch(modes[0]) {
			case 'r':
				fprintf(stderr, "[" KBLD KGRN "NODE%2d" KNRM "]" KBLD KRED "ERROR: File '" KBLD KWHT "%s" KNRM KRED "' couldn't be opened for reading. Exiting.\r\n" KNRM, rank, filepath);
				break;
			case 'w':
				fprintf(stderr, "[" KBLD KGRN "NODE%2d" KNRM "]" KBLD KRED "ERROR: File '" KBLD KWHT "%s" KNRM KRED "' couldn't be opened for writing. Exiting.\r\n" KNRM, rank, filepath);
				break;
			default:
				fprintf(stderr, "[" KBLD KGRN "NODE%2d" KNRM "]" KBLD KRED "ERROR: File '" KBLD KWHT "%s" KNRM KRED "' couldn't be opened. INCORRECT MODE. Exiting.\r\n" KNRM, rank, filepath);
				break;
		}
		exit(-1);
	}
	
	return file_to_open;
}

inline int char_allowed(char c) {
	if ((c >= 'a' && c <= 'z') ||
		(c >= 'A' && c <= 'Z') ||
		(c >= '0' && c <= '9')) {
		return 1;
	}
	else {
		return 0;
	}
}

inline FILE* open_minifile(char* minifiles_location, char* filepath, int rank, int* part) {
	FILE* file_w = NULL;

	char* filename = NULL;
	char* file_w_path = NULL;
	int file_w_pathlen;

	filename = get_last_token(filepath, '/');
	file_w_pathlen = strlen(minifiles_location) + strlen(filename) + strlen("_node") + 2 + strlen("_part") + 3 + 1;

	file_w_path = (char*) safe_malloc(sizeof(char) * file_w_pathlen, file_w_path, "file_w_name (in divide_files_into_minifiles)");
	sprintf(file_w_path, "%s%s_part%03d", minifiles_location, filename, *part);

	(*part)++;

	file_w = safe_fopen(file_w_path, "w", rank);

	safe_free(filename, "filename (in open_minifile)");
	safe_free(file_w_path, "file_w_path (in open_minifile)");

	return file_w;
}

inline int process_file_into_minifile(FILE* file_r, FILE* file_w, int to_n_chars) {
	char c;

	int chars = 0;
	int space_allowed = 1;
	int enter_allowed = 1;
	while ( (c = fgetc(file_r)) != EOF) {
		if (chars >= to_n_chars && !char_allowed(c)) {
			fputc('\0', file_w);
			break;
		}

		if (char_allowed(c)) {
			fputc(c, file_w);
			chars++;
			space_allowed = 1;
			enter_allowed = 1;
		}
		else if (c == ' ' && space_allowed) {
			fputc(c, file_w);
			chars++;
			space_allowed = 0;
		}
		else if (c == '\r' && enter_allowed) {
			fputc(c, file_w);
			chars++;
		}
		else if (c == '\n' && enter_allowed) {
			fputc(c, file_w);
			chars++;
			enter_allowed = 0;
		}
	}

	if (c == EOF) {
		return 1;
	}
	else {
		return 0;
	}
}

inline void report_nothing_to_divide(int workload_amount, int rank, int extra_prints) {
	if (!workload_amount && extra_prints) {
		fprintf(stderr, "[" KBLD KGRN "NODE%2d" KNRM "]" KYEL " I have nothing to map." KNRM "\r\n", rank);
	}
}

inline void report_minifile_no_sending(int status, int root, int rank, char* filepath, int minifiles_no, int extra_prints) {
	if (status == 0 && extra_prints) {
		char* filename = get_last_token(filepath, '/');
		printf("[" KBLD KGRN "NODE%2d" KNRM "] Divided '" KBLD KWHT "%s" KNRM "' into " KBLD KGRN "%d" KNRM " minifiles "
			"and reported back to root " KBLD KBLU "%d" KNRM ".\r\n", rank, filename, minifiles_no, root);
		safe_free(filename, "filename (from divide_received_files_and_report_back)");
	}
	else if (status != 0) {
		fprintf(stderr, "[" KBLD KGRN "NODE%2d" KNRM "] " KRED "Couldn't reach root " KBLD KBLU "%d" KNRM KRED " for minifile split report. Exiting." KNRM "\r\n", rank, root);
		exit(-1);
	}
}

int divide_file_into_minifiles(char* filepath, int rank, int of_n_chars);

#endif
