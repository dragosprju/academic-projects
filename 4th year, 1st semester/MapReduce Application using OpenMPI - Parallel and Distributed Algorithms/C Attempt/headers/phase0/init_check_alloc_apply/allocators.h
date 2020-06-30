#ifndef allocators_h
#define allocators_h

inline NODES* allocate_workload_amounts(NODES* nodes) {
	nodes->workload_amount = (int*) safe_calloc(sizeof(int), nodes->no_nodes, nodes->workload_amount, "nodes->workload_amount");

	return nodes;
}

inline FILES* allocate_dirpath(FILES* files, char* dirpath) {
	int len = strlen(dirpath);

	files->dirpath = (char*) safe_malloc(sizeof(char) * len, files->dirpath, "files->dirpath");

	return files;
}

inline FILES* allocate_filepaths(FILES* files) {
	int i;

	files->filepaths = (char**) safe_malloc((sizeof(char*) * files->no_files), files->filepaths, "files->filepaths");
	for (i = 0; i < files->no_files; i++) {
		files->filepaths[i] = NULL;
	}

	return files;
}

inline FILES* allocate_file_destinations(FILES* files) {
	int i;

	files->file_destinations = (int*) safe_malloc(sizeof(int) * files->no_files, files->file_destinations, "files->file_destinations");
	for (i = 0; i < files->no_files; i++) {
		files->file_destinations[i] = -1;
	}

	return files;
}

#endif
