#include <comik.h>
#include <global_memmap.h>
#include <5kk03-utils.h>

#include <hw_dma.h>
#include <fifo.h>
#include <memmap.h>
#include "structures.h"

volatile int counter= 0;

typedef struct 
{
	FBlock fbout[10];
	int leftover; 
} IDCTToken;

typedef struct
{
	PBlock pbout[10];
} CCToken;
//MB2


int main (void)
{



	// Enable stack checking.
	start_stack_check();



	hw_declare_dmas(1);
	DMA *dma = hw_dma_add(1, (int*)mb2_dma0_BASEADDR );
	int dma_num = 1;

	FCB *MB_12_FBlock = fifo_add(
	MB2_12_FIFO_WC,
	MB2_12_FIFO_RC,
	MB2_12_FIFO_RWC,
	MB2_12_FIFO_RRC,
	MB2_12_FIFO_BUFFER,
	MB2_12_FIFO_RBUFFER,
	7,
	sizeof(IDCTToken),
	1,1,
	1,
	1,
	0,
	0);

	FCB *MB_21_PBlock = fifo_add(
	MB2_21_FIFO_WC,
	MB2_21_FIFO_RC,
	MB2_21_FIFO_RWC,
	MB2_21_FIFO_RRC,
	MB2_21_FIFO_BUFFER,
	MB2_21_FIFO_RBUFFER,
	7,
	sizeof(CCToken),
	1,1,
	1,
	1,
	0,
	0);
	// Sync with the monitor.
	mk_mon_sync();

	fifo_set_consumption_token_rate(MB_12_FBlock, 1);
	fifo_set_production_token_rate(MB_21_PBlock, 1);
	int pboutCalculated = 0;
	int pblocksSend = 0;
	int leftover = 9999;

	while(1){
		if(fifo_check_data(MB_12_FBlock)){
			volatile IDCTToken *idctToken = fifo_claim_data(MB_12_FBlock);
			fifo_pull(MB_12_FBlock);
			while(!fifo_check_space(MB_21_PBlock));
			volatile PBlock *pboutp = fifo_claim_space(MB_21_PBlock);
			
			// mk_mon_debug_info(idctToken->leftover);
			//do stuff with it

			for(int i = 0; i<10; i++){
				idct( &(idctToken->fbout[i]), &(pboutp[i]));  //pbout[pboutCalculated%10][i]				
			}

			fifo_push(MB_21_PBlock);
			fifo_release_data(MB_21_PBlock);
			leftover = idctToken->leftover;
			fifo_release_space(MB_12_FBlock);
			pboutCalculated++;

		}

		// if(fifo_check_space(MB_21_PBlock) && (pblocksSend < pboutCalculated)){
		// 	volatile PBlock *pboutp = fifo_claim_space(MB_21_PBlock);
		// 	memcpy(pboutp, pbout, (10* sizeof(PBlock)));
		// 	// mk_mon_debug_info((int)pboutp->linear[8]);
		// 	fifo_push(MB_21_PBlock);
		// 	fifo_release_data(MB_21_PBlock);
		// 	pblocksSend++;
		// }

		if(leftover == 0){
			while(hw_dma_status(dma));
			break;
		}
	}




    // Your code here

	// Signal the monitor we are done.
	mk_mon_debug_tile_finished();
	return 0;
}
