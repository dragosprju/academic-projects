/******************************************************************************
*
* Copyright (C) 2009 - 2014 Xilinx, Inc.  All rights reserved.
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* Use of the Software is limited solely to applications:
* (a) running on a Xilinx device, or
* (b) that interact with a Xilinx device through a bus or interconnect.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
* XILINX  BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
* WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF
* OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*
* Except as contained in this notice, the name of the Xilinx shall not be used
* in advertising or otherwise to promote the sale, use or other dealings in
* this Software without prior written authorization from Xilinx.
*
******************************************************************************/

/*
 * helloworld.c: simple test application
 *
 * This application configures UART 16550 to baud rate 9600.
 * PS7 UART (Zynq) is not initialized by this application, since
 * bootrom/bsp configures it to baud rate 115200
 *
 * ------------------------------------------------
 * | UART TYPE   BAUD RATE                        |
 * ------------------------------------------------
 *   uartns550   9600
 *   uartlite    Configurable only in HW design
 *   ps7_uart    115200 (configured by bootrom/bsp)
 */

#include <stdio.h>
#include "platform.h"
#include "xil_printf.h"

#include "queue_hw_1/queue_hw_1.h"

#define CPUNAME "mb1"
#define CPUID 'B'

queue_t* intra = (queue_t *)0x43630000; // Intra
queue_t* iese  = (queue_t *)0x43640000; // Iese

void sleep(int val) {
	int i;
	for (i = 0; i < val; i++);
}

void preliminary_test() {
    char to_receive[2];

	queue_get(intra, to_receive, 1);
	to_receive[1] = '\0';
	queue_put(iese, to_receive, 1);
}

void empty_queues() {
	char foobar;

	while(!queue_is_empty(intra)) {
		queue_get_once(intra, &foobar);
	}

	while(!queue_is_empty(iese)) {
		queue_get_once(iese, &foobar);
	}
}

int main()
{
    init_platform();

    preliminary_test();
    empty_queues();

    char gotCPUID = -1;
    char leader = CPUID;
    int ok = 0;

    // Pasul 1
    queue_put(iese, &leader, 1); // Right now leader is our own ID

    while (!ok) {
    	// Pasul 2
    	queue_get(intra, &gotCPUID, 1);

    	if (gotCPUID < CPUID) {
    		leader = gotCPUID;
    		queue_put(iese, &leader, 1);
    	}

    	// Pasul 3
    	if (gotCPUID == CPUID) {
    		queue_put(iese, "x", 1);
    		ok = 1;
    	}

    	if (gotCPUID == 'x') {
    		ok = 1;
    	}
    }

    cleanup_platform();
    return 0;
}
