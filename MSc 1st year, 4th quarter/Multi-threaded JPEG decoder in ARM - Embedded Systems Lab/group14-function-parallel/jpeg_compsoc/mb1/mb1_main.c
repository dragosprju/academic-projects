#include <comik.h>
#include <global_memmap.h>
#include <5kk03-utils.h>
#include <hw_dma.h>
#include <fifo.h>
#include <memmap.h>
#include "structures.h"

//MB1
int main (void){
	typedef struct 
	{
	    FValue fv[10];
	    SubHeader1 sh1;
	    SubHeader2 sh2;
	} VLDToken;
	volatile VLDToken vldtoken;

	typedef struct 
	{
		FBlock fbout[10];
		int leftover; 
	} IDCTToken;




	// Enable stack checking.
	start_stack_check();

	hw_declare_dmas(1);
	DMA *dma = hw_dma_add(1, (int*)mb1_dma0_BASEADDR );

	int dma_num = 0;
	#if HAS_DMA
		dma_num = 1;
	#endif

	FCB *MB_31_VLD = fifo_add(
	MB1_31_FIFO_WC,
	MB1_31_FIFO_RC,
	MB1_31_FIFO_RWC,
	MB1_31_FIFO_RRC,
	MB1_31_FIFO_BUFFER,
	MB1_31_FIFO_RBUFFER,
	2,
	(828 * sizeof(int)),
	1,1,
	dma_num,
	1,
	0,
	0);


	FCB *MB_12_FBLOCK = fifo_add(
	MB1_12_FIFO_WC,
	MB1_12_FIFO_RC,
	MB1_12_FIFO_RWC,
	MB1_12_FIFO_RRC,
	MB1_12_FIFO_BUFFER,
	MB1_12_FIFO_RBUFFER,
	7,
	sizeof(IDCTToken),
	1,1,
	dma_num,
	1,
	0,
	0);

	FCB *MB_21_PBlock = fifo_add(
	MB1_21_FIFO_WC,
	MB1_21_FIFO_RC,
	MB1_21_FIFO_RWC,
	MB1_21_FIFO_RRC,
	MB1_21_FIFO_BUFFER,
	MB1_21_FIFO_RBUFFER,
	7,
	10* sizeof(PBlock),
	1,1,
	1,
	1,
	0,
	0);
	// Sync with the monitor.
	mk_mon_sync();

	volatile PBlock pbout[10];
	volatile FBlock fbout[10];
	volatile SubHeader2 sh2[10];
	volatile SubHeader1 sh1[10];

	volatile int counter= 0;
	volatile int leftover = 9999;
	volatile int leftoverEnd = 9999;
	volatile int tokensReceivedFromMB3 = 0;
	volatile int tokensReceivedFromMB2 = 0;
	volatile int tokensSendToMB2 = 0;

	fifo_set_production_token_rate(MB_12_FBLOCK, 1);
	fifo_set_consumption_token_rate(MB_31_VLD, 1);
	fifo_set_consumption_token_rate(MB_21_PBlock, 1);

	

	while(1){
		if(fifo_check_data(MB_31_VLD) && ((tokensReceivedFromMB3 - tokensReceivedFromMB2) < 6) && fifo_check_space(MB_12_FBLOCK)){
			volatile VLDToken *vldTokenp = fifo_claim_data(MB_31_VLD);
			fifo_pull(MB_31_VLD);	
			leftover = vldTokenp->sh2.leftover;
			sh2[tokensReceivedFromMB3%10] = vldTokenp->sh2;
			sh1[tokensReceivedFromMB3%10] = vldTokenp->sh1;
			volatile IDCTToken *idctToken = fifo_claim_space(MB_12_FBLOCK);
			idctToken->leftover = leftover;
			//mk_mon_debug_info(leftover);
			for(int i = 0; i < 10; i++){
				iqzz( &(vldTokenp->fv[i]), &(idctToken->fbout[i]));
				// idct( &fbout[tokensReceivedFromMB3%10], &(pbout[i]));
			}
			// mk_mon_debug_info(leftover);
			fifo_push(MB_12_FBLOCK);
			fifo_release_data(MB_12_FBLOCK);
			fifo_release_space(MB_31_VLD);  
			tokensReceivedFromMB3++;
		}


		if(fifo_check_data(MB_21_PBlock) && (tokensReceivedFromMB2 < tokensReceivedFromMB3)){
			volatile PBlock *pboutp = fifo_claim_data(MB_21_PBlock);
			fifo_pull(MB_21_PBlock);

			ColorBuffer cbout;
	        cc( &(sh1[tokensReceivedFromMB2%10]), &(pboutp[0]), &cbout);
	        raster(&(sh2[tokensReceivedFromMB2%10]), &cbout, dma );  
	        leftoverEnd = sh2[tokensReceivedFromMB2%10].leftover;
			// mk_mon_debug_info(leftoverEnd);
			fifo_release_space(MB_21_PBlock);
			tokensReceivedFromMB2++;
		}


        if (leftoverEnd == 0){
            while(hw_dma_status(dma));	
            break;
        }
	
	
	}
	// Signal the monitor we are done.
	mk_mon_debug_tile_finished();
	return 0;
}
