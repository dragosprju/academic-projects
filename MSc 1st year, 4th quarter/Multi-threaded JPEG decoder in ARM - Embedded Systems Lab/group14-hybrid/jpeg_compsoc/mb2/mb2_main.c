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
    FValue fv[10];
    SubHeader1 sh1;
    SubHeader2 sh2;
} TokenInfo32;

typedef struct 
{
    PBlock pbout[10];
    SubHeader1 sh1;
    SubHeader2 sh2;
} TokenInfo24;

volatile TokenInfo32 ti32;
volatile TokenInfo24 ti24;

	// Enable stack checking.
	start_stack_check();
	hw_declare_dmas(1);
	DMA *dma = hw_dma_add(1, (int*)mb2_dma0_BASEADDR );
	int dma_num = 0;

#if HAS_DMA
	dma_num = 1;
#endif

	FCB *fcb32 = fifo_add(
	MB2_32_FIFO_WC,
	MB2_32_FIFO_RC,
	MB2_32_FIFO_RWC,
	MB2_32_FIFO_RRC,
	MB2_32_FIFO_BUFFER,
	MB2_32_FIFO_RBUFFER,
	2,
	sizeof(ti32),
	1,1,
	dma_num,
	1,
	0,
	0);

	
	FCB *fcb24 = fifo_add(
	MB2_24_FIFO_WC,
	MB2_24_FIFO_RC,
	MB2_24_FIFO_RWC,
	MB2_24_FIFO_RRC,
	MB2_24_FIFO_BUFFER,
	MB2_24_FIFO_RBUFFER,
	2,
	sizeof(ti24),
	1,1,
	dma_num,
	1,
	0,
	0);
	

	// Sync with the monitor.
	mk_mon_sync();

	volatile PBlock pbout[10];
	fifo_set_consumption_token_rate(fcb32, 1);
	fifo_set_production_token_rate(fcb24, 1);
	
	int leftover = 9999;
	while(1){
		while(!fifo_check_data(fcb32));
		volatile TokenInfo32 *tip32 = fifo_claim_data(fcb32);
		fifo_pull(fcb32);

		while(!fifo_check_space(fcb24));
        volatile TokenInfo24 *tip24 = fifo_claim_space(fcb24);
        
        leftover = tip32->sh2.leftover;

        if(leftover == -1){
        	fifo_release_space(fcb32);

        	tip24->sh2.leftover = -1;

        	fifo_push(fcb24);
        	fifo_release_data(fcb24); 
        	break;
        }

		for(int i = 0; i < 10; i++){
			FBlock fbout;
			iqzz( &(tip32->fv[i]), &fbout);
			idct( &fbout, &(tip24->pbout[i]));
		}

        tip24->sh1 = tip32->sh1;
        tip24->sh2 = tip32->sh2;

        fifo_push(fcb24);
        fifo_release_data(fcb24);     
        fifo_release_space(fcb32);

        if ( leftover <= 1 ){
        	while(!fifo_check_space(fcb24));
        	volatile TokenInfo24 *tip24 = fifo_claim_space(fcb24);
        	
        	tip24->sh2.leftover = -1;

        	fifo_push(fcb24);
        	fifo_release_data(fcb24); 
            break;
        }  
	}
	while(hw_dma_status(dma));
	// Signal the monitor we are done.
	mk_mon_debug_tile_finished();
	return 0;
}
