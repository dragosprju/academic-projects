# Enable DMA mode (instead of MMIO)
USE_DMA=1
OPTFLAGS=-O3 -Wall -std=c99 -Wl,--gc-sections 


# download image, or not
DO_IMAGE_OUTPUT?=1
IMAGE_OUTPUT_WIDTH?=1024  
IMAGE_OUTPUT_HEIGHT?=768

# code for each tile.
MB1_DIR=mb1
MB2_DIR=mb2
MB3_DIR=mb3
MB4_DIR=mb4

# shared code and header files.
MB_SHARED=mb_shared

ifeq (${USE_DMA},1)
$(info DMA mode enabled)
OPTFLAGS+=-DHAS_DMA=1
else
$(info DMA mode disabled)
endif



# Upload files before the run
DATA_FILES?=images/noise.jpg

# force use of FPGA board connected to VGA framegrabber
USE_VGA_GRABBER?=1

# force image to be downloaded in binary form (not Huffman encoded)
#FORCE_BINARY?=1

# specify timeout
#USER_TIMEOUT?=30

include /home/emb18/material/platform/platform.mk
