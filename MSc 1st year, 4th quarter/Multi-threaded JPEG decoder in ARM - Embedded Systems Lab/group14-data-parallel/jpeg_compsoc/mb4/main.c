#include <comik.h>
#include <global_memmap.h>
#include <5kk03-utils.h>
#include <hw_dma.h>
#include <multicore.h>
#include "structures.h"
#include "actors.h"

volatile unsigned int *output_mem = (unsigned int*) shared_pt_REMOTEADDR;
volatile unsigned int *shared_mem = (unsigned int*) (shared_pt_REMOTEADDR + 4*1024*1024);
//volatile unsigned char *remote_buffer = (unsigned int*) (shared_pt_REMOTEADDR + 4*1024*1024 + 512*1024);

volatile unsigned int *fb   = (unsigned int *)shared_pt_REMOTEADDR;
volatile unsigned int *dma_mem = (unsigned int *)mb4_dmamem0_BASEADDR; 

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

    // Sync with the monitor.
    mk_mon_sync();
    // Enable stack checking.
    start_stack_check();
    mk_mon_debug_info(4444);
    DMA dma = {0, mb4_dma0_BASEADDR };

    startScreen(&dma);




    static VldHeader header;
    // Start decoding the JPEG.
    init_header_vld ( &header, shared_mem, output_mem);

    int size = header.mx_size * header.my_size;
    int begin = 3 * ((size + CORES - 1) / CORES);
    int end = begin + ((size + CORES - 1) / CORES);


    static SubHeader2 sh2;
    int cnt = 0;

    while(1)
    {   
        static FValue fv[10];
        static SubHeader1 sh1;
        header_vld ( &header, &header, &(fv[0]), &sh1, &sh2 );

        if (cnt >= begin && cnt < end){
            static PBlock pbout[10];
            for ( int i = 0; i < 10 ; i++ )
            {
                static FBlock fbout;
                iqzz( &(fv[i]), &fbout);
                idct( &fbout, &(pbout[i]));
            }

            static ColorBuffer cbout;
            cc( &sh1, &(pbout[0]), &cbout);
            raster( &sh2, &cbout );
        }

        if ( sh2.leftover == 0 ){
            break;
        }
        cnt++;
    }
    // Wait untill all writes are finished
    while(hw_dma_status(&dma));

    // Signal the monitor we are done.
    mk_mon_debug_tile_finished();
    return 0;
}
