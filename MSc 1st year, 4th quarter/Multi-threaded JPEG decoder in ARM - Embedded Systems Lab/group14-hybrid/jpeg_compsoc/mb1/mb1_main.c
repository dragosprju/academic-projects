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
} TokenInfo31;

typedef struct 
{
    PBlock pbout[10];
    SubHeader1 sh1;
    SubHeader2 sh2;
} TokenInfo14;

volatile TokenInfo31 ti31;
volatile TokenInfo14 ti14;

	// Enable stack checking.
	start_stack_check();
	hw_declare_dmas(1);
	DMA *dma = hw_dma_add(1, (int*)mb1_dma0_BASEADDR );
	int dma_num = 0;

#if HAS_DMA
	dma_num = 1;
#endif

	FCB *fcb31 = fifo_add(
	MB1_31_FIFO_WC,
	MB1_31_FIFO_RC,
	MB1_31_FIFO_RWC,
	MB1_31_FIFO_RRC,
	MB1_31_FIFO_BUFFER,
	MB1_31_FIFO_RBUFFER,
	2,
	sizeof(ti31),
	1,1,
	dma_num,
	1,
	0,
	0);

	
	FCB *fcb14 = fifo_add(
	MB1_14_FIFO_WC,
	MB1_14_FIFO_RC,
	MB1_14_FIFO_RWC,
	MB1_14_FIFO_RRC,
	MB1_14_FIFO_BUFFER,
	MB1_14_FIFO_RBUFFER,
	2,
	sizeof(ti14),
	1,1,
	dma_num,
	1,
	0,
	0);
	

	// Sync with the monitor.
	mk_mon_sync();

	volatile PBlock pbout[10];
	fifo_set_consumption_token_rate(fcb31, 1);
	fifo_set_production_token_rate(fcb14, 1);
	
	int leftover;

	while(1){
		while(!fifo_check_data(fcb31));
		volatile TokenInfo31 *tip31 = fifo_claim_data(fcb31);
		fifo_pull(fcb31);

		while(!fifo_check_space(fcb14));
        volatile TokenInfo14 *tip14 = fifo_claim_space(fcb14);

		for(int i = 0; i < 10; i++){
			FBlock fbout;
			iqzz( &(tip31->fv[i]), &fbout);
			idct( &fbout, &(tip14->pbout[i]));
		}

        tip14->sh1 = tip31->sh1;
        tip14->sh2 = tip31->sh2;
        leftover = tip14->sh2.leftover;

        fifo_push(fcb14);
        fifo_release_data(fcb14);     
        fifo_release_space(fcb31);      

   		if ( leftover <= 1 ){
			while(!fifo_check_space(fcb14));
        	volatile TokenInfo14 *tip14 = fifo_claim_space(fcb14);
        	
        	tip14->sh2.leftover = -1;

        	fifo_push(fcb14);
        	fifo_release_data(fcb14);  
            break;
        }  
	}
	while(hw_dma_status(dma));
	// Signal the monitor we are done.
	mk_mon_debug_tile_finished();
	return 0;
}
