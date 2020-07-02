#include <comik.h>
#include <global_memmap.h>
#include <5kk03-utils.h>
#include <hw_dma.h>
#include <memmap.h>
#include <fifo.h>
#include "structures.h"

volatile unsigned int *output_mem = (unsigned int*) shared_pt_REMOTEADDR;
volatile unsigned int *shared_mem = (unsigned int*) (shared_pt_REMOTEADDR + 4*1024*1024);
volatile unsigned char *remote_buffer = (unsigned int*) (shared_pt_REMOTEADDR + 4*1024*1024 + 512*1024);

volatile unsigned int *fb   = (unsigned int *)shared_pt_REMOTEADDR;
volatile unsigned int *dma_mem = (unsigned int *)mb3_dmamem0_BASEADDR + 2*sizeof(int); 


void startScreen(DMA *dma){
    // Paint it our 'group' color so we can identify it.
    for(int y = 0; y< 768; y++){
        if(y < 768/3){
            for ( int i = 0; i < 1024; i++) {
                dma_mem[i] = 0xFFAE1C28;
            }
        }else if(y < (768/3)*2){
             for ( int i = 0; i < 1024; i++) {
                dma_mem[i] = 0xFFFFFFFF;
            }
        }else{
             for ( int i = 0; i < 1024; i++) {
                dma_mem[i] = 0xFF21468B;
            }
        }
    hw_dma_send(&fb[y*1024], dma_mem, 1024, dma, NON_BLOCKING);
    }
    while(hw_dma_status(dma));
}



int main (void)
{
    typedef struct 
    {
        FValue fv[10];
        SubHeader1 sh1;
        SubHeader2 sh2;
    } TokenInfo;

    static volatile TokenInfo ti;

    // Enable stack checking.
    start_stack_check();
    hw_declare_dmas(1);
    DMA *dma = hw_dma_add(1, (int*)mb3_dma0_BASEADDR);

    static int dma_num = 0;
    
    #if HAS_DMA
        dma_num = 1;
    #endif

    FCB *fcb31= fifo_add(
            MB3_31_FIFO_WC,
            MB3_31_FIFO_RC,
            MB3_31_FIFO_RWC,
            MB3_31_FIFO_RRC,
            MB3_31_FIFO_BUFFER,
            MB3_31_FIFO_RBUFFER,
        2,
        sizeof(ti),
        1,1,
        dma_num,
        1,
        0,
        0);
    
    FCB *fcb32 = fifo_add(
            MB3_32_FIFO_WC,
            MB3_32_FIFO_RC,
            MB3_32_FIFO_RWC,
            MB3_32_FIFO_RRC,
            MB3_32_FIFO_BUFFER,
            MB3_32_FIFO_RBUFFER,
        2,
        sizeof(ti),
        1,1,
        dma_num,
        1,
        0,
        0);
    
    // Sync with the monitor.
    mk_mon_sync();
    startScreen(dma);

    static volatile VldHeader header;
    // Start decoding the JPEG.
    int leftover = 9999;
    init_header_vld ( &header, shared_mem, output_mem);

    fifo_set_production_token_rate(fcb31, 1);
    fifo_set_production_token_rate(fcb32, 1);

    int cnt = 0;
    while(1){
        if (cnt % 2 == 0){
            while(!fifo_check_space(fcb31));
            volatile TokenInfo *tip = fifo_claim_space(fcb31);

            header_vld ( &header, &header, &(tip->fv[0]), &(tip->sh1), &(tip->sh2) );
            leftover = tip->sh2.leftover;
            fifo_push(fcb31);
            fifo_release_data(fcb31);
        }

        if (cnt % 2 == 1){
            while(!fifo_check_space(fcb32));
            volatile TokenInfo *tip = fifo_claim_space(fcb32);

            header_vld ( &header, &header, &(tip->fv[0]), &(tip->sh1), &(tip->sh2) );
            leftover = tip->sh2.leftover;
            fifo_push(fcb32);
            fifo_release_data(fcb32);
        }

        if ( leftover == 0 ){
            if (header.mx_size * header.my_size == 1){
                while(!fifo_check_space(fcb32));
                volatile TokenInfo *tip = fifo_claim_space(fcb32);
                tip->sh2.leftover = -1;
                fifo_push(fcb32);
                fifo_release_data(fcb32);
            }
            break;
        }
        cnt++;
    }
    while(hw_dma_status(dma));
    // Signal the monitor we are done.
    mk_mon_debug_tile_finished();
    return 0;
}
