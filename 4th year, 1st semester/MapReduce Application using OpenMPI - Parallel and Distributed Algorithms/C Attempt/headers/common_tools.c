#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

#include "defines.h"
#include "common_tools.h"

extern int allocated_pointers;

void sleep_ms(int milliseconds) // cross-platform sleep function
{
#ifdef WIN32
    Sleep(milliseconds);
#elif _POSIX_C_SOURCE >= 199309L
    struct timespec ts;
    ts.tv_sec = milliseconds / 1000;
    ts.tv_nsec = (milliseconds % 1000) * 1000000;
    nanosleep(&ts, NULL);
#else
    usleep(milliseconds * 1000);
#endif
}

/* !!! STRTOK IS __DESTRUCTIVE__ !!! */
char* get_last_token(char* string, char sep) {
	int i;

	int last_i_with_sep = 0;
	for (i = 1; i < strlen(string); i++) {
		if (string[i] == sep) {
			last_i_with_sep = i;
		}
	}

	int new_string_len = strlen(string) + 1 - last_i_with_sep;
	char* new_string = NULL;
	new_string = safe_malloc(sizeof(char) * new_string_len, new_string, "new_string (in get_last_token)");

	for (i = 0; i < new_string_len; i++) {
		new_string[i] = string[last_i_with_sep + i + 1];
	}
	new_string[i] = '\0';

	return new_string;
}

extern inline void check_all_pointers_deallocated(int rank, int actually_print);

void* safe_malloc(size_t size, void* ptr, char* name) {
	if (ptr == NULL) {
		ptr = malloc(size);

		if (ptr == NULL) {
			fprintf(stderr, KRED KBLD "ERROR" KNRM KRED ": Pointer " KBLD KWHT "%s" KNRM KRED " couldn't be allocated.\r\n" KNRM, name);
		}

		allocated_pointers++;

		return ptr;
	}
	else {
		fprintf(stderr, KRED KBLD "ERROR" KNRM KRED ": Pointer " KBLD KWHT "%s" KNRM KRED " to be allocated is not " KBLU "NULL" KRED ".\r\n" KNRM, name);
		exit(-1);
	}
}

void* safe_calloc(size_t size, int no_elem, void* ptr, char* name) {
	if (ptr == NULL) {
		ptr = calloc(size, no_elem);

		if (ptr == NULL) {
			fprintf(stderr, KRED KBLD "ERROR" KNRM KRED ": Pointer " KBLD KWHT "%s" KNRM KRED " couldn't be callocated.\r\n" KNRM, name);
		}

		allocated_pointers++;

		return ptr;
	}
	else {
		fprintf(stderr, KRED KBLD "ERROR" KNRM KRED ": Pointer " KBLD KWHT "%s" KNRM KRED " to be callocated is not " KBLU "NULL" KRED ".\r\n" KNRM, name);
		exit(-1);
	}
}

void safe_free(void *ptr, char* name) {
	if (ptr != NULL) {
		free(ptr);
		ptr = NULL;

		allocated_pointers--;

	}
	else {
		fprintf(stderr, KRED KBLD "ERROR" KNRM KRED ": Pointer " KBLD KWHT "%s" KNRM KRED " to be freed is " KBLU KWHT "NULL" KRED ".\r\n" KNRM, name);
		exit(-1);
	}
}


// Good for messages without any %s or %d to change
void print_message(MSG_TYPE message_type, char* message, int node, int actually_print) {
	if (!actually_print) return;

	if (message_type == MSG_ERROR) {
		if (node == -1) {
			fprintf(stderr, KBLD KRED "ERROR" KNRM KRED ": %s\r\n" KNRM, message);
		}
		else if (node == ROOT) {
			fprintf(stderr, "[" KBLU KBLD "ROOT" KNRM "] " KRED KBLD "ERROR" KNRM KRED ": %s\r\n" KNRM, message);
		}
		else {
			printf("About to print!\r\n");
			fprintf(stderr, "[" KGRN KBLD "NODE%2d" KNRM "] " KBLD KRED "ERROR" KNRM KRED ": %s\r\n" KNRM, node, message);
		}
	}
	else if (message_type == MSG_WARN) {
		if (node == -1) {
			fprintf(stderr, KYEL KRED "WARNING" KNRM KYEL ": %s\r\n" KNRM, message);
		}
		else if (node == ROOT) {
			fprintf(stderr, "[" KBLU KBLD "ROOT" KNRM "] " KYEL KBLD "WARNING" KNRM KYEL ": %s\r\n" KNRM, message);
		}
		else {
			fprintf(stderr, "[" KGRN KBLD "NODE%2d" KNRM "] " KYEL KBLD "WARNING" KNRM KYEL ": %s\r\n" KNRM, node, message);
		}
	}
	else if (message_type == MSG_NORM) {
		if (node == -1) {
			fprintf(stderr, "%s\r\n" KNRM, message);
		}
		else if (node == ROOT) {
			fprintf(stderr, "[" KBLU KBLD "ROOT" KNRM "] %s\r\n" KNRM, message);
		}
		else {
			fprintf(stderr, "[" KGRN KBLD "NODE%2d" KNRM "] %s\r\n" KNRM, node, message);
		}
	}
	else {
		fprintf(stderr, KBLD KRED "Error" KNRM KRED ": Unknown message type during 'print_message' call.\r\n" KNRM);
	}
}