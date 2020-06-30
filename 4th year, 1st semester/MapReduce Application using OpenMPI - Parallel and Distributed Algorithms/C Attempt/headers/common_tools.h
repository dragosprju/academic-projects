#ifndef common_tools_h
#define common_tools_h

#include <stdio.h>
#include "defines.h"
#include "typedefs.h"

int allocated_pointers;

void sleep_ms(int milliseconds);
char* get_last_token(char* string, char sep);

inline void check_all_pointers_deallocated(int rank, int actually_print) {
	if (allocated_pointers > 0 && actually_print) {
		fprintf(stderr, "[" KGRN KBLD "NODE%2d" KNRM "] " KYEL KBLD "WARNING" KNRM KYEL ": There are %d pointers still allocated at program end.\r\n" KNRM, rank, allocated_pointers);
	}
}
void* safe_malloc(size_t size, void* ptr, char* name);
void* safe_calloc(size_t size, int no_elem, void* ptr, char* name);
void safe_free(void *ptr, char* name);

void print_message(MSG_TYPE message_type, char* message, int node, int actually_print);

#endif