#include "structures.h"
#include "stdarg.h"
#include "5kk03.h"
#include <hw_dma.h>
#include <global_memmap.h>

int vld_count = 0;

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
    DMA dma = {0, mb3_dma0_BASEADDR };
    // Make sure that DMAmem is not overwritten in raster function, so using higher address
    volatile unsigned int *dma_mem = (unsigned int *)mb3_dmamem0_BASEADDR + 0xCF3; 
    static int index_loaded = 9999;
	  int index = 0;

    index = (fp->vld_count >> 2) - index_loaded;

    if(index < 0 || index >= 32){
      index_loaded = fp->vld_count >> 2 ;
      hw_dma_receive(dma_mem, &fp->data[fp->vld_count >> 2], 32, &dma, BLOCKING);
      index = 0;
    }

    unsigned int c = ((dma_mem[index] << (8 * (3 - (fp->vld_count % 4))) ) >> 24 ) & 0x00ff;
    
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
