#include <string.h>
#include "../../common_tools.h"
#include "../../defines.h"
#include "allocators.h"

extern inline NODES* allocate_workload_amounts(NODES* nodes);
extern inline FILES* allocate_dirpath(FILES* files, char* dirpath);
extern inline FILES* allocate_filepaths(FILES* files);
extern inline FILES* allocate_file_destinations(FILES* files);
