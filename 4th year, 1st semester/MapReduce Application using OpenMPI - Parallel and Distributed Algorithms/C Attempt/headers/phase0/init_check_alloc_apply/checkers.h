#ifndef checkers_h
#define checkers_h

#include <string.h>
#include <dirent.h>

#include "../../common_tools.h"
#include "../../defines.h"

inline int is_it_dots(struct dirent* filepath) {
	if (!strcmp (filepath->d_name, ".")) {
		return 1;
	}
	if (!strcmp (filepath->d_name, "..")) {
		return 1;
	}

	return 0;
}

void check_directory_can_be_read(char* dirpath, int extra_prints);
int check_files_exist(char* dirpath, int extra_prints);

inline void check_if_theres_an_argument(int argc) {
	if (argc < 2) {
		print_message(MSG_ERROR, "You haven't included an argument (the directory to index) when calling the program.", -1, 1);
	}
}

inline void check_num_processes(int no_nodes) {
	if (no_nodes < 2) {
		char* message = NULL;
		sprintf(message, "More than 2 processes should be provided as argument for '" KBLD KWHT "-np" KNRM KRED "'.");
		print_message(MSG_ERROR, message, -1, 1);
		exit(-1);	
	}
}

inline void check_root_validity(int root, int no_nodes) {
	if (ROOT >= no_nodes) {
		char* message = NULL;
		sprintf(message, "The " KNRM KBLU "root" KRED " rank is bigger than the number of processes provided for '-np'.");
		print_message(MSG_ERROR, message, -1, 1);
	}
}

#endif
