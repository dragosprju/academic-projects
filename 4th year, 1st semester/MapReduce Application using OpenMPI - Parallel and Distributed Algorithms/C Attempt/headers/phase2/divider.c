#include "divider.h"
#include "../defines.h"

#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include "../common_tools.h"

extern inline FILE* safe_fopen(char* filepath, char* modes, int rank);
extern inline int char_allowed(char c);
extern inline FILE* open_minifile(char* minifiles_location, char* filepath, int rank, int* part);
extern inline int process_file_into_minifile(FILE* file_r, FILE* file_w, int to_n_chars);
extern inline void report_minifile_no_sending(int status, int root, int rank, char* filepath, int minifiles_no, int extra_prints);
extern inline void report_nothing_to_divide(int workload_amount, int rank, int extra_prints);


int divide_file_into_minifiles(char* filepath, int rank, int of_n_chars) {
	FILE* file_r = NULL;
	FILE* file_w = NULL;

	int finished = 0;
	int minifile_no = 0;

	file_r = safe_fopen(filepath, "r", rank);

	while(!finished) {
		file_w = open_minifile(WORKDIR, filepath, rank, &minifile_no);
		finished = process_file_into_minifile(file_r, file_w, of_n_chars);
	}

	fclose(file_r);
	fclose(file_w);	

	return minifile_no;
}
