#ifndef free_ers_h
#define free_ers_h

inline void free_files(FILES* files) {
	int filei;

	for (filei = 0; filei < files->no_files; filei++) {
		safe_free(files->filepaths[filei], "files->filepaths[filei] (in free_files)");
	}
	safe_free(files->dirpath, "files->dirpath (in free_files)");
	safe_free(files->filepaths, "files->filepaths (in free_files)");
	safe_free(files->file_destinations, "files->file_destinations (in free_files)");
	safe_free(files, "files (in free_files)");
}

inline void free_nodes(NODES* nodes) {
	safe_free(nodes->workload_amount, "node->workload_amount[nodei] (in free_files)");
	safe_free(nodes, "nodes (in free_files)");
}

inline void free_filepaths(char** filepaths, int no_files) {
	int filei;
	for (filei = 0; filei < no_files; filei++) {
		safe_free(filepaths[filei], "filepaths[filei] (in main, for child nodes)");
	}
	safe_free(filepaths, "filepaths (in main, for child nodes)");
}

#endif