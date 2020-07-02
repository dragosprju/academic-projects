#ifdef PC
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#endif
#include <hw_dma.h>
#include "structures.h"
#include <global_memmap.h>

typedef union RGBA
{
    unsigned int data;
    struct pixel
    {
        unsigned char b, g, r, a;
    } pixel;
}RGBA;

void raster ( const SubHeader2 * SH2, const ColorBuffer * CB )
{
    const int     goodrows    = SH2->goodrows;
    const int     goodcolumns = SH2->goodcolumns;
    const int     MCU_sx      = SH2->MCU_sx;
    const int     n_comp      = SH2->n_comp;
    int           i, j;
    const int     x_size = 1024;

    int dmaAdressCounter = 0;
    int checkOffsets = 1;
    int lastRowOffset;// = (SH2->MCU_row * SH2->MCU_sy ) * x_size;;
    int lastColOffset;// =  ( SH2->MCU_column - 1 ) * SH2->MCU_sx;
    
    static int id_raster = -1;
    volatile RGBA *mem = (RGBA *) SH2->fp.fb;
    volatile RGBA *dma_mem = (RGBA *) mb3_dmamem0_BASEADDR; 
    DMA dma = {0, mb3_dma0_BASEADDR };

    if (id_raster != -1){
        while(!hw_dma_check_send_complete(&dma, id_raster));
    }
    

    
    for ( i = 0; i < goodrows; i++ ) {
        const int row_offset = ( i + SH2->MCU_row * SH2->MCU_sy ) * x_size;
        for ( j = 0; j < goodcolumns; j++ ) {
            int col_offset = j + ( SH2->MCU_column - 1 ) * SH2->MCU_sx;
            if(checkOffsets == 1){
                lastRowOffset = row_offset;
                lastColOffset = col_offset;
                checkOffsets = 0;
            }
            if ( n_comp == 3 ) {
                dma_mem[dmaAdressCounter].pixel.r = CB->data[n_comp * i * MCU_sx + 2 + j * n_comp];
                dma_mem[dmaAdressCounter].pixel.g = CB->data[n_comp * i * MCU_sx + 1 + j * n_comp];
                dma_mem[dmaAdressCounter].pixel.b = CB->data[n_comp * i * MCU_sx + 0 + j * n_comp];
            }else if ( n_comp == 1 ) {
                dma_mem[dmaAdressCounter].pixel.r  = CB->data[n_comp * i * MCU_sx + 0 + j * n_comp];
                dma_mem[dmaAdressCounter].pixel.g  = CB->data[n_comp * i * MCU_sx + 0 + j * n_comp];
                dma_mem[dmaAdressCounter].pixel.b  = CB->data[n_comp * i * MCU_sx + 0 + j * n_comp];
            }
            dmaAdressCounter++;
            if(dmaAdressCounter == 32){
                id_raster = hw_dma_send(&mem[lastRowOffset + lastColOffset] , &dma_mem[0], dmaAdressCounter, &dma, NON_BLOCKING);
                dmaAdressCounter = 0;
                checkOffsets = 1;
            } 
        }
        if(dmaAdressCounter != 0){
            id_raster = hw_dma_send(&mem[lastRowOffset + lastColOffset] , &dma_mem[0], dmaAdressCounter, &dma, NON_BLOCKING);
            dmaAdressCounter = 0;
            checkOffsets = 1;
        }
    }
}
