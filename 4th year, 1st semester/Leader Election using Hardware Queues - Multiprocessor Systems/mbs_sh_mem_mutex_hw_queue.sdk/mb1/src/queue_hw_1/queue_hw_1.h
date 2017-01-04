#ifndef _QUEUE_
#define _QUEUE_

#include "xil_types.h"

typedef struct {
	u32 write_ptr;
	u32 reserved1;
	u32 read_ptr;
	u32 reserved2;
	u32 status;
	u32 error;
} queue_t;

int queue_is_full(queue_t *q);
int queue_is_empty(queue_t *q);

int queue_put_once(queue_t *q, char* data);
int queue_get_once(queue_t *q, char* data);
int queue_put_unblocked(queue_t *q, char* data, int len);
int queue_get_unblocked(queue_t *q, char* data, int len);
int queue_put(queue_t *q, char* data, int len);
int queue_get(queue_t *q, char* data, int len);
#endif
