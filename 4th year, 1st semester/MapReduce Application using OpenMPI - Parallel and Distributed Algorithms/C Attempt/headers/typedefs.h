#ifndef typedefs_h
#define typedefs_h

typedef struct {
	int root;
	int no_nodes;
	int* workload_amount;
	int global_node_index;
} NODES;

typedef struct {
	int no_files;
	char* dirpath;
	char** filepaths;
	int* file_destinations;
	int* minifile_counts;
} FILES;

typedef enum {
	EXIT,
	RECEIVE_FILEPATHS
} COMMAND;

typedef enum {
	MSG_NORM,
	MSG_ERROR,
	MSG_WARN
} MSG_TYPE;

#endif
