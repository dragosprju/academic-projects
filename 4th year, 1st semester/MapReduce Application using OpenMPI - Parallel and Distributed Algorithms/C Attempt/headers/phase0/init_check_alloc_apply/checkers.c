#include <stdio.h>
#include <stdlib.h>
#include <dirent.h>
#include <string.h>

#include "checkers.h"
#include "../files.h"
#include "../../defines.h"

extern inline int is_it_dots(struct dirent* filepath);

extern inline void check_if_theres_an_argument(int argc);
extern inline void check_num_processes(int no_nodes);
extern inline void check_root_validity(int root, int no_nodes);

void check_directory_can_be_read(char* dirpath, int extra_prints) {
	DIR* dir;

	/* Presupunem că verificarea directoriului în sine 
	 * (încât să nu eșueze opendir și closedir) s-a făcut deja. */
	dir = opendir(dirpath);
	if (dir== NULL) {
		print_message(MSG_ERROR, "The directory you have specified is incorrect.", -1, 1);
		exit(-1);
	}
	else {
		print_message(MSG_NORM, "Directory successfully opened.", -1, extra_prints);
	}
	closedir(dir);
}

int check_files_exist(char* dirpath, int extra_prints) {
	DIR* dir;
	struct dirent* dirfile;
	int no_files;

	/* Presupunem că verificarea directoriului în sine 
	 * (încât să nu eșueze opendir și closedir) s-a făcut deja. */
	dir = opendir(dirpath);
	no_files = 0;

	while((dirfile = readdir(dir))) {
		/* Evităm procesarea '.' și '..' */
		if(is_it_dots(dirfile)) continue;
		no_files++;
	}

	closedir(dir);

	if (no_files == 0) {
		print_message(MSG_ERROR, "The directory you have selected is empty. Cannot continue.", -1, 1);
		exit(-1);
	}
	else {
		print_message(MSG_NORM, "There are files inside the directory.", -1, extra_prints);
	}

	return no_files;
}
