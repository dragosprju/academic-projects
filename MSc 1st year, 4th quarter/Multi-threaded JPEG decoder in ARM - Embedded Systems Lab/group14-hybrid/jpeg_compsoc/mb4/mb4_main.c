#include <comik.h>
#include <global_memmap.h>
#include <5kk03-utils.h>
#include <hw_dma.h>
#include <fifo.h>
#include <memmap.h>
#include "structures.h"

//MB2
int main (void){

typedef struct 
{
    PBlock pbout[10];
    SubHeader1 sh1;
    SubHeader2 sh2;
} TokenInfo;


volatile TokenInfo ti;

	// Enable stack checking.
	start_stack_check();
	hw_declare_dmas(1);
	DMA *dma = hw_dma_add(1, (int*)mb4_dma0_BASEADDR );
	int dma_num = 0;

#if HAS_DMA
	dma_num = 1;
#endif
	
	FCB *fcb14 = fifo_add(
	MB4_14_FIFO_WC,
	MB4_14_FIFO_RC,
	MB4_14_FIFO_RWC,
	MB4_14_FIFO_RRC,
	MB4_14_FIFO_BUFFER,
	MB4_14_FIFO_RBUFFER,
	2,
	sizeof(ti),
	1,1,
	dma_num,
	1,
	0,
	0);

	
	FCB *fcb24 = fifo_add(
	MB4_24_FIFO_WC,
	MB4_24_FIFO_RC,
	MB4_24_FIFO_RWC,
	MB4_24_FIFO_RRC,
	MB4_24_FIFO_BUFFER,
	MB4_24_FIFO_RBUFFER,
	2,
	sizeof(ti),
	1,1,
	dma_num,
	1,
	0,
	0);
	

	// Sync with the monitor.
	mk_mon_sync();

	fifo_set_consumption_token_rate(fcb14, 1);
	fifo_set_consumption_token_rate(fcb24, 1);
	
	int leftover1 = 9999;
	int leftover2 = 9999;

	while(1){
		if(fifo_check_data(fcb14)){
			volatile TokenInfo *tip14 = fifo_claim_data(fcb14);
			fifo_pull(fcb14);		
			
	        leftover1 = tip14->sh2.leftover;

	        if (leftover1 != -1){
				ColorBuffer cbout;
		        cc( &(tip14->sh1), &(tip14->pbout[0]), &cbout);
		        raster(&(tip14->sh2), &cbout);    
	        }
	        fifo_release_space(fcb14);
    	}

    	if(fifo_check_data(fcb24)){
			volatile TokenInfo *tip24 = fifo_claim_data(fcb24);
			fifo_pull(fcb24);		
			
	        leftover2 = tip24->sh2.leftover;

	        if (leftover2 != -1){
				ColorBuffer cbout;
		        cc( &(tip24->sh1), &(tip24->pbout[0]), &cbout);
		        raster(&(tip24->sh2), &cbout);    
	    	}	        
	        fifo_release_space(fcb24);
    	}

        if ( leftover1 == -1 && leftover2 == -1 ){
            break;
        }
	}
	while(hw_dma_status(dma));
	// Signal the monitor we are done.
	mk_mon_debug_tile_finished();
	return 0;
}
