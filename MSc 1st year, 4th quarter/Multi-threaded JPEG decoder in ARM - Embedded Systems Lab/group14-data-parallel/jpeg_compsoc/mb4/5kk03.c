#include "structures.h"
#include "stdarg.h"
#include "5kk03.h"
#include <hw_dma.h>
#include <global_memmap.h>

//int vld_count = 0;


void my_fprintf ( FILE *stream, char *d, ... )
{
#ifdef PC
    va_list list;
    va_start ( list, d );
    vfprintf ( stream, d, list );
    va_end ( list );
#endif
}

unsigned int FGETC ( JPGFile *fp)
{
    DMA dma = {0, mb4_dma0_BASEADDR };
    // Make sure that DMAmem is not overwritten in raster function, so using higher address
    volatile unsigned int * dma_mem[2] = {mb4_dmamem0_BASEADDR + 0x600, mb4_dmamem0_BASEADDR + 0x700}; 

    static int index_loaded = 9999;
    static int preFetchIndex;

    static unsigned char workingOn = 0;
    static unsigned char check[2];
    static unsigned char id[2];
  
    int index = (fp->vld_count >> 2) - index_loaded;

    // Reset both parts of DMA memory
    if (workingOn == 0 || index < 0 || index >= 64){
      workingOn = 1;
      check[0] = 1;
      check[1] = 1;
     
      index_loaded = fp->vld_count >> 2;
      id[0] = hw_dma_receive(dma_mem[0], &fp->data[fp->vld_count >> 2], 32, &dma, NON_BLOCKING);
      id[1] = hw_dma_receive(dma_mem[1], &fp->data[(fp->vld_count >> 2) + 32], 32, &dma, NON_BLOCKING);

      index = 0;
      preFetchIndex = index_loaded + 32;
    }

    // Switch between parts of DMA memory
    if(index >= 32 && index < 64){
      // Start preloading
      id[workingOn-1] = hw_dma_receive(dma_mem[workingOn-1], &fp->data[preFetchIndex + 32], 32, &dma, NON_BLOCKING);
      check[workingOn-1] = 1;

      if(workingOn == 1)
        workingOn = 2;
      else
        workingOn = 1;

      index_loaded = preFetchIndex;
      preFetchIndex += 32;
      index = (fp->vld_count >> 2) - index_loaded;
    }

    if(check[workingOn-1] == 1){
      while(!hw_dma_check_receive_complete(&dma, id[workingOn-1]));
      check[workingOn-1] = 0;
    }

    unsigned int c = ((dma_mem[workingOn-1][index] << (8 * (3 - (fp->vld_count % 4))) ) >> 24 ) & 0x00ff;

    fp->vld_count++;

    return c;
}

int FSEEK ( JPGFile *fp, int offset, int start )
{
    fp->vld_count += offset + ( start - start );    /* Just to use start... */
    return 0;
}

size_t FTELL ( JPGFile *fp )
{
    return fp->vld_count;
}
