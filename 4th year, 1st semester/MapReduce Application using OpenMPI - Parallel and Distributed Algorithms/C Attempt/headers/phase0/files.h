#ifndef files_h
#define files_h

#include <stdio.h>
#include <string.h>
#include "../common_tools.h"
#include "../typedefs.h"

char* construct_string_from_three(char* string1, char* string2, char* string3);

void make_filepaths_list(FILES* files, int extra_prints);
void delete_unreadable_filepaths(FILES* files, int extra_prints);

inline int select_next_node(int curr_node, int root, int no_nodes) {
	do {
		curr_node += 1;
		if (curr_node >= no_nodes) {
			curr_node = 0;
		}
	} while(curr_node == root);

	return curr_node;
}

void round_robin_files(FILES* files, NODES* nodes, int extra_prints);

#endif
