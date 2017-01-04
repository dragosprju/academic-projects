#include "queue_hw_0.h"

#define FIRST_BIT 0x01
#define SECOND_BIT 0x02

int queue_is_full(queue_t* q) {
	return (q->status & SECOND_BIT) == 2;
}

int queue_is_empty(queue_t* q) {
	return (q->status & FIRST_BIT) == 1;
}

int queue_put_once(queue_t *q, char* data) {
	if (!queue_is_full(q)) {
		q->write_ptr = *data;
		return 0;
	}
	else {
		return -1;
	}
}

int queue_get_once(queue_t *q, char* data) {
	if (!queue_is_empty(q)) {
		*data = q->read_ptr;
		return 0;
	}
	else {
		return -1;
	}
}

// Returneaza len daca s-a facut corect
// Returneaza indexul sub len daca s-a gresit
int queue_put_unblocked(queue_t *q, char* data, int len) {
	int i;
	int status;
	for (i = 0; i < len; i++) {
		status = queue_put_once(q, data+i);
		if (status != 0) {
			return i;
		}
	}
	return len;
}

// La fel ca sus
int queue_get_unblocked(queue_t *q, char* data, int len) {
	int i;
	int status;
	for (i = 0; i < len; i++) {
		status = queue_get_once(q, data+i);
		if (status != 0) {
			return i;
		}
	}
	return len;
}

int queue_put(queue_t *q, char* data, int len) {
	while(queue_put_unblocked(q, data, len) != len);
}

int queue_get(queue_t *q, char* data, int len) {
	while(queue_get_unblocked(q, data, len) != len);
}
