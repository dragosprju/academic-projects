#ifndef MEMMAP_H
#define MEMMAP_H

#define TokenAmount = 3
#define Buffer1 = 828 * sizeof(int)
#define Buffer2 = 65* sizeof(int)

// =========================Transfer 1====================================================
#define MB3_31_FIFO_WC  	  (int*)(mb3_dmamem0_BASEADDR)								//MB3dma  0
#define MB3_31_FIFO_RC  	  (int*)(mb3_cmem0_BASEADDR)								//MB3cmem 0
#define MB3_31_FIFO_RWC 	  (int*)(mb1_cmem0_pt_REMOTEADDR)							//MB1cmem 0
#define MB3_31_FIFO_RRC  	  (int*)(NULL)
#define MB3_31_FIFO_BUFFER    (int*)(mb3_dmamem0_BASEADDR+sizeof(int))					//MB3BUFFER sender (VLD)
#define MB3_31_FIFO_RBUFFER   (int*)(mb1_cmem0_pt_REMOTEADDR+ (3* sizeof(int)))			//MB1BUFFER receiver (VLD)


#define MB1_31_FIFO_WC  	 (int*)(mb1_cmem0_BASEADDR)									//MB1cmem 0
#define MB1_31_FIFO_RC  	 (int*)(mb1_dmamem0_BASEADDR)								//MB1DMA  0						
#define MB1_31_FIFO_RWC 	 (int*)(NULL)
#define MB1_31_FIFO_RRC 	 (int*)(mb3_cmem0_pt_REMOTEADDR)							//MB3cmem 0
#define MB1_31_FIFO_BUFFER   (int*)(mb1_cmem0_BASEADDR+ (3* sizeof(int)))					//MB1 BUFFER receiver (VLD)
#define MB1_31_FIFO_RBUFFER  (int*)(NULL)


// =========================Transfer 2====================================================
#define MB1_12_FIFO_WC  	  (int*)(mb1_dmamem0_BASEADDR + sizeof(int)	)			//MB1dma  1 
#define MB1_12_FIFO_RC  	  (int*)(mb1_cmem0_BASEADDR+sizeof(int))				//MB1cmem 1
#define MB1_12_FIFO_RWC 	  (int*)(mb2_cmem0_pt_REMOTEADDR)							//MB2cmem 0
#define MB1_12_FIFO_RRC  	  (int*)(NULL)
#define MB1_12_FIFO_BUFFER    (int*)(mb1_dmamem0_BASEADDR + (36 * sizeof(int)))		    //MB1BUFFER sender (test)
#define MB1_12_FIFO_RBUFFER   (int*)(mb2_cmem0_pt_REMOTEADDR+ (2 * sizeof(int)))			//MB1BUFFER receiver (test)


#define MB2_12_FIFO_WC  	 (int*)(mb2_cmem0_BASEADDR)									//MB2cmem 0
#define MB2_12_FIFO_RC  	 (int*)(mb2_dmamem0_BASEADDR)								//MB2dma 0
#define MB2_12_FIFO_RWC 	 (int*)(NULL)
#define MB2_12_FIFO_RRC 	 (int*)(mb1_cmem0_pt_REMOTEADDR + sizeof(int))				//MB1cmem 1
#define MB2_12_FIFO_BUFFER   (int*)(mb2_cmem0_BASEADDR+ 2* sizeof(int))					//MB2BUFFER receiver (test)
#define MB2_12_FIFO_RBUFFER  (int*)(NULL)

// =========================Transfer 3====================================================

#define MB2_21_FIFO_WC  	  (int*)(mb2_dmamem0_BASEADDR + sizeof(int))				//MB1dma  1 
#define MB2_21_FIFO_RC  	  (int*)(mb2_cmem0_BASEADDR+    sizeof(int))			//MB1cmem 1
#define MB2_21_FIFO_RWC 	  (int*)(mb1_cmem0_pt_REMOTEADDR + (2* sizeof(int)))			//MB2cmem 0
#define MB2_21_FIFO_RRC  	  (int*)(NULL)
#define MB2_21_FIFO_BUFFER    (int*)(mb2_dmamem0_BASEADDR+ (3* sizeof(int)))				//MB1BUFFER sender (test)
#define MB2_21_FIFO_RBUFFER   (int*)(mb1_cmem0_pt_REMOTEADDR + (4* sizeof(int)) + (7* 828 * sizeof(int)))	//MB1BUFFER receiver (test)


#define MB1_21_FIFO_WC  	 (int*)(mb1_cmem0_BASEADDR	+ (2* sizeof(int)))				//MB2cmem 0
#define MB1_21_FIFO_RC  	 (int*)(mb1_dmamem0_BASEADDR + (2 * sizeof(int)))						//MB2dma 0
#define MB1_21_FIFO_RWC 	 (int*)(NULL)
#define MB1_21_FIFO_RRC 	 (int*)(mb2_cmem0_pt_REMOTEADDR + sizeof(int))			//MB1cmem 1
#define MB1_21_FIFO_BUFFER   (int*)(mb1_cmem0_BASEADDR + (4* sizeof(int)) + (7 * 828 * sizeof(int)))					//MB2BUFFER receiver (test)
#define MB1_21_FIFO_RBUFFER  (int*)(NULL)





//============================OTHER MEMORY USAGE===========================================
//MB3 FGETC -> dma1 , no conflict
#define MB1_RASTER_BUFFER (int*)(mb1_dmamem0_BASEADDR + (3* sizeof(int)))

#endif
