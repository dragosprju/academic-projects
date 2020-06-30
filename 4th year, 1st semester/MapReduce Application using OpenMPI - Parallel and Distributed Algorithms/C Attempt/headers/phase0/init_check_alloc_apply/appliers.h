#ifndef appliers_h
#define appliers_h

#include <string.h>
#include "../../common_tools.h"
#include "../../defines.h"

inline NODES* apply_nodes_values(int root, int no_nodes, NODES* nodes) {
	nodes->root = root;
	nodes->no_nodes = no_nodes;
	nodes->global_node_index = 0;

	return nodes;
}

inline FILES* apply_files_values(int no_files, char* dirpath, FILES* files) {
	files->no_files = no_files;
	strcpy(files->dirpath, dirpath);
	
	return files;
}

#endif
