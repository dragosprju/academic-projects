#ifndef MEMMAP_H
#define MEMMAP_H

#define FIFO_OFFSET (0x1000)
#define MB3_31_FIFO_WC  	  	(int*)(mb3_dmamem0_BASEADDR)
#define MB3_31_FIFO_RWC 	  	(int*)(mb1_cmem0_pt_REMOTEADDR)
#define MB3_31_FIFO_RC  	  	(int*)(mb3_cmem0_BASEADDR)
#define MB3_31_FIFO_RRC  	  	(int*)(NULL)
#define MB3_31_FIFO_BUFFER    	(int*)(mb3_dmamem0_BASEADDR+2*sizeof(int))
#define MB3_31_FIFO_RBUFFER   	(int*)(mb1_cmem0_pt_REMOTEADDR+2*sizeof(int))

#define MB1_31_FIFO_WC  	 	(int*)(mb1_cmem0_BASEADDR)
#define MB1_31_FIFO_RWC 	 	(int*)(NULL)
#define MB1_31_FIFO_RC  	 	(int*)(mb1_dmamem0_BASEADDR)
#define MB1_31_FIFO_RRC 	 	(int*)(mb3_cmem0_pt_REMOTEADDR)
#define MB1_31_FIFO_BUFFER   	(int*)(mb1_cmem0_BASEADDR+2*sizeof(int))
#define MB1_31_FIFO_RBUFFER  	(int*)(NULL)


#define MB3_32_FIFO_WC  	  	(int*)(mb3_dmamem0_BASEADDR+sizeof(int))
#define MB3_32_FIFO_RWC 	  	(int*)(mb2_cmem0_pt_REMOTEADDR)
#define MB3_32_FIFO_RC  	  	(int*)(mb3_cmem0_BASEADDR+sizeof(int))
#define MB3_32_FIFO_RRC  	  	(int*)(NULL)
#define MB3_32_FIFO_BUFFER    	(int*)(mb3_dmamem0_BASEADDR+2*sizeof(int)+1*(10*sizeof(FValue)+sizeof(SubHeader1)+sizeof(SubHeader2)))
#define MB3_32_FIFO_RBUFFER   	(int*)(mb2_cmem0_pt_REMOTEADDR+2*sizeof(int))

#define MB2_32_FIFO_WC  	 	(int*)(mb2_cmem0_BASEADDR)
#define MB2_32_FIFO_RWC 	 	(int*)(NULL)
#define MB2_32_FIFO_RC  	 	(int*)(mb2_dmamem0_BASEADDR)
#define MB2_32_FIFO_RRC 	 	(int*)(mb3_cmem0_pt_REMOTEADDR+sizeof(int))
#define MB2_32_FIFO_BUFFER   	(int*)(mb2_cmem0_BASEADDR+2*sizeof(int))
#define MB2_32_FIFO_RBUFFER  	(int*)(NULL)


#define MB1_14_FIFO_WC 			(int*)(mb1_dmamem0_BASEADDR+sizeof(int))
#define MB1_14_FIFO_RWC 	   	(int*)(mb4_cmem0_pt_REMOTEADDR)
#define MB1_14_FIFO_RC  	   	(int*)(mb1_cmem0_BASEADDR+sizeof(int))
#define MB1_14_FIFO_RRC  	   	(int*)(NULL)
#define MB1_14_FIFO_BUFFER     	(int*)(mb1_dmamem0_BASEADDR+2*sizeof(int))
#define MB1_14_FIFO_RBUFFER    	(int*)(mb4_cmem0_pt_REMOTEADDR+2*sizeof(int))

#define MB4_14_FIFO_WC  	 	(int*)(mb4_cmem0_BASEADDR)
#define MB4_14_FIFO_RWC 	 	(int*)(NULL)
#define MB4_14_FIFO_RC  	 	(int*)(mb4_dmamem0_BASEADDR)
#define MB4_14_FIFO_RRC 	 	(int*)(mb1_cmem0_pt_REMOTEADDR+sizeof(int))
#define MB4_14_FIFO_BUFFER   	(int*)(mb4_cmem0_BASEADDR+2*sizeof(int))
#define MB4_14_FIFO_RBUFFER  	(int*)(NULL)


#define MB2_24_FIFO_WC 			(int*)(mb2_dmamem0_BASEADDR+sizeof(int))
#define MB2_24_FIFO_RWC 	   	(int*)(mb4_cmem0_pt_REMOTEADDR+sizeof(int))
#define MB2_24_FIFO_RC  	   	(int*)(mb2_cmem0_BASEADDR+sizeof(int))
#define MB2_24_FIFO_RRC  	   	(int*)(NULL)
#define MB2_24_FIFO_BUFFER     	(int*)(mb2_dmamem0_BASEADDR+2*sizeof(int))
#define MB2_24_FIFO_RBUFFER    	(int*)(mb4_cmem0_pt_REMOTEADDR+2*sizeof(int)+2*(10*sizeof(PBlock)+sizeof(SubHeader1)+sizeof(SubHeader2)))

#define MB4_24_FIFO_WC  	 	(int*)(mb4_cmem0_BASEADDR+sizeof(int))
#define MB4_24_FIFO_RWC 	 	(int*)(NULL)
#define MB4_24_FIFO_RC  	 	(int*)(mb4_dmamem0_BASEADDR+sizeof(int))
#define MB4_24_FIFO_RRC 	 	(int*)(mb2_cmem0_pt_REMOTEADDR+sizeof(int))
#define MB4_24_FIFO_BUFFER   	(int*)(mb4_cmem0_BASEADDR+2*sizeof(int)+2*(10*sizeof(PBlock)+sizeof(SubHeader1)+sizeof(SubHeader2)))
#define MB4_24_FIFO_RBUFFER  	(int*)(NULL)

#endif
