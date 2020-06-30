#ifndef initializers_h
#define initializers_h

#include "../../common_tools.h"
#include "../../defines.h"

inline NODES* initialize_nodes(NODES* nodes) {
	nodes = (NODES*) safe_malloc(sizeof(NODES), nodes, "nodes");
	nodes->workload_amount = NULL;

	return nodes;
}

inline FILES* initialize_files(FILES* files) {
	files = (FILES*) safe_malloc(sizeof(FILES), files, "files");
	files->dirpath = NULL;
	files->filepaths = NULL;
	files->file_destinations = NULL;

	return files;
}

#endif
