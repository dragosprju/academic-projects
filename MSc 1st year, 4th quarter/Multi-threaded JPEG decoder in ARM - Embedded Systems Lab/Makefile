.EXPORT_ALL_VARIABLES:
LC_CTYPE=en_US.UTF-8
LC_ALL=en_US.UTF-8

.DEFAULT_GOAL := one

BUILD_DIR?=_initial_code
SHELL:=/bin/bash
MAKEFILE_DIR=$(CURDIR)
USER_TIMEOUT_SETTING=600
TIMEOUT=900

RESULTS_DIR=benchmark_results/results_files
OUT_PICTURES_DIR=output_pictures
OUT_FILES_DIR=output_responses

SUBDIRS := $(wildcard [0-9]_*/.)
PICTURES := $(wildcard benchmark_files/test_pictures/$(SELECT)*)
OUT_PICTURES := $(wildcard $(BUILD_DIR)/$(OUT_PICTURES_DIR)/$(SELECT)*)
OUT_FILES := $(wildcard $(BUILD_DIR)/$(OUT_FILES_DIR)/*$(SELECT).out)
BUILD_DIRS ?= $(wildcard _*/.)

# Gotta fix: 

COMMIT ?= HEAD
FOLDER ?= _initial_code
BRANCH ?= master

# ifndef VERBOSE
# .SILENT:
# endif

# ifndef VERBOSE
# 	MAKE_ARGS=--silent
# endif

.PHONY: generate_pictures $(PICTURES) compare_pictures $(OUT_PICTURES) interpret_results $(OUT_FILES) tail checkpoint_before checkpoint_after all $(BUILD_DIRS)

init:
	@git submodule update --init --recursive

update:
	@git submodule update --recursive

clone:
	@-git clone -b master git@gitlab.tue.nl:5lib0-2017/group14.git -b $(BRANCH) $(FOLDER)
	@-cd $(FOLDER) && git reset --hard $(COMM)
	@echo ""
	@echo ""

recreate:
	# 0a. _initial_code
	#    master / Starting point, all code provided by 5LIB0 (Nick)
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_initial_code BRANCH=master COMM=78035cd6 # being HEAD

	# 0b. _initial_code_1_core
	#    master / Starting point, all code provided by 5LIB0 (Nick)
	@#$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_initial_code BRANCH=master COMM=78035cd6 # being HEAD
	@#echo "_dp_pingpong_v1_dma_1_core needs to be switched from 1 cores to 2"
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_initial_code_1_core BRANCH=_initial_code_1_core_proper COMM=HEAD # being HEAD

	# 1. _dp_simple_mmio_2_cores
	# 	 nick_playground / Data parallel - 2 cores (Nick)
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_simple_mmio_2_cores BRANCH=nick_playground COMM=5a3d44b6
	#@echo "_dp_rowread_dma_1_core needs editing"	

	# 2. _dp_simple_mmio_4_cores
	# 	 nick_playground / Data parallel - 4 cores (Nick)
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_simple_mmio_4_cores BRANCH=nick_playground COMM=25617f41

	# 3. _dp_simple_dma_1_core_fixed
	#    _dp_simple_dma_1_core_fixed / BUGFIX - sizeof(RGBA) -> 1 (Nick)
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_simple_dma_1_core BRANCH=_dp_simple_dma_1_core_fixed COMM=5e05ffa4 #being HEAD

	# 4. _dp_rowread_dma_1_core
	# 	 Data_parallel_DMA / updated binary_original for compare and updated makefile (Kristiaan)
	#        earlier it is: optimized the raster to do entire rows at the time (Kristiaan)
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_rowread_dma_1_core BRANCH=Data_parallel_DMA COMM=a422a3ef9
	@#echo "_dp_rowread_dma_1_core needs editing"

	# 5. _dp_prefetch_dma_1_core
	# 	 kristiaan_playground / DMA: BLOCKING -> NON_BLOCKING (Nick)
	#	              earlier it is: FGETC - load 32 words simultaneously
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_prefetch_dma_1_core BRANCH=Data_parallel_DMA COMM=30c739fa 
	@#echo "_dp_prefetch_dma_1_core needs editing"

	# 6. _dp_pingpong_v1_dma_1_core
	# 	 kristiaan_playground / DMA - prefetching (Nick)
	@#$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_pingpong_v1_dma_1_core BRANCH=kristiaan_playground COMM=9c548aa4
	@#echo "_dp_pingpong_v1_dma_1_core needs to be switched from just mb1 to mb1 and mb3"
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_pingpong_v1_dma_1_core BRANCH=_dp_pingpong_v1_dma_1_core_proper COMM=HEAD

	# 7. _dp_pingpong_v1_dma_mb1
	# 	 kristiaan_playground / DMA - prefetching (Nick)
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_pingpong_v1_dma_mb1 BRANCH=kristiaan_playground COMM=9c548aa4

	# 8. _dp_pingpong_v2_dma_1_core
	# 	 Data_parallel_DMA / bugfix - red line visible in output image (Nick)
	@#$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_pingpong_v2_dma_1_core BRANCH=Data_parallel_DMA COMM=39ec25ac
	@#echo "_dp_pingpong_v2_dma_1_core needs to be switched from 4 cores to mb1 and mb3"
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_pingpong_v2_dma_1_core BRANCH=_dp_pingpong_v2_dma_1_core_proper COMM=HEAD
	
	# 9. _dp_pingpong_v2_dma_4_cores
	# Data_parallel_DMA / Parallel optimization, separate CORE and SIZE defines <- selected new
	# Data_parallel_DMA / bugfix - red line visible in output image (Nick) <-- broken without startScreen(..)
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_pingpong_v2_dma_4_cores BRANCH=Data_parallel_DMA COMM=e452e2db

	# 10. _dp_pingpong_v3_dma_1_core
	# Data_parallel_DMA / fix rounding errors (Nick)
	@#$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_pingpong_v3_dma_1_core BRANCH=Data_parallel_DMA COMM=8049fb82 #being HEAD
	@#echo "_dp_pingpong_v3_dma_1_core needs to be switched from 4 cores to mb1 and mb3"
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_pingpong_v3_dma_1_core BRANCH=_dp_pingpong_v3_dma_1_core_proper COMM=HEAD

	# 11. _dp_pingpong_v3_dma_4_cores
	# Data_parallel_DMA / fix rounding errors (Nick)
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_dp_pingpong_v3_dma_4_cores BRANCH=Data_parallel_DMA COMM=8049fb82 #being HEAD

	# 12. _fp_v1_mb1ToMb2
	# kristiaan_functional_Paralellism / removed debug code 
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_fp_v1_mb1ToMb2 BRANCH=kristiaan_functionalParalellism COMM=53e83c21 #being HEAD

	# 13. _fp_v2_mb3ToMb1
	# kristiaan_functional_Paralellism_mb3tomb1 / looked at the mysterious no error 3 parameter into 2 parameter mystery 
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_fp_v2_mb3ToMb1 BRANCH=kristiaan_functionalParalellism_mb3tomb1 COMM=800f5574 #being HEAD

	# 14. _fp_v3_3cores
	# functional_paralelism_3cores / updated the fifo buffer and memmap space (Kristiaan)
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_fp_v3_3cores BRANCH=functional_paralelism_3cores COMM=5d331216 # being HEAD

	# 15. _hybrid_v1_mb1+3_mb2+4
	# Hybrid_13_24 / Bugfix - benchmark images (Nick)
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_hybrid_v1_mb1+3_mb2+4 BRANCH=Hybrid_13_24 COMM=38cb6951 # being HEAD

	# 16. _hybrid_v2_mb3_mb1+2_mb4
	# Hybrid_3_12_4_update / Bugfix - blue image (Nick)
	@$(MAKE) -C $(MAKEFILE_DIR) clone FOLDER=_hybrid_v2_mb3_mb1+2_mb4 BRANCH=Hybrid_3_12_4_update COMM=131a200a  # being HEAD

olevel:
	@grep --include=\Makefile -rnw '$(MAKEFILE_DIR)' -e "-O0"

one:
	@find  -type f  -name "Makefile" -exec touch {} +
	@printf "\e[1;94m$(BUILD_DIR):\e[0m\n"
	@printf "\e[1;97mBuilding & running...\e[0m\n"
	@$(MAKE) -C $(MAKEFILE_DIR) generate_pictures BUILD_DIR=$(BUILD_DIR) SELECT=$(SELECT)
	@printf "\n"
	@printf "\e[1;97mInterpreting results...\e[0m\n"
	@$(MAKE) -C $(MAKEFILE_DIR) interpret_results BUILD_DIR=$(BUILD_DIR) SELECT=$(SELECT)
	@printf "\n"
	@printf "\e[1;97mValidating output pictures...\e[0m\n"
	@$(MAKE) -C $(MAKEFILE_DIR) compare_pictures BUILD_DIR=$(BUILD_DIR) SELECT=$(SELECT)
	@printf "\n"

generate_pictures: $(PICTURES)
$(PICTURES): 
	$(eval WIDTH := $(shell identify -format %w $@))
	$(eval HEIGHT := $(shell identify -format %h $@))

	$(eval FILENAME := $(shell echo $@ | cut -c 31- | rev | cut -c 5- | rev | tr -d '\n'))

	@mkdir -p $(BUILD_DIR)/$(OUT_PICTURES_DIR)
	@mkdir -p $(BUILD_DIR)/$(OUT_FILES_DIR)
	@-rm -f $(BUILD_DIR)/$(OUT_PICTURES_DIR)/$(FILENAME).pnm
	@-rm -f $(BUILD_DIR)/$(OUT_FILES_DIR)/$(BUILD_DIR)_$(FILENAME).out
	@$(MAKE) -C $(BUILD_DIR)/jpeg_compsoc clean $(MAKE_ARGS) 2>/dev/null >/dev/null
	@-timeout --foreground ${TIMEOUT} $(MAKE) -C $(BUILD_DIR)/jpeg_compsoc run $(MAKE_ARGS) \
	  DO_IMAGE_OUTPUT=1 \
	  IMAGE_OUTPUT_WIDTH=$(WIDTH) \
	  IMAGE_OUTPUT_HEIGHT=$(HEIGHT) \
	  DATA_FILES=$(MAKEFILE_DIR)/$@ \
	  USER_TIMEOUT=$(USER_TIMEOUT_SETTING) \
	  2>/dev/null > $(BUILD_DIR)/$(OUT_FILES_DIR)/$(BUILD_DIR)_$(FILENAME).out \
	  & timeout --foreground ${TIMEOUT} python benchmark_files/wait.py $(BUILD_DIR)/$(OUT_FILES_DIR)/$(BUILD_DIR)_$(FILENAME).out $(FILENAME)
	@sed -r "s,\x1B\[[0-9;]*[a-zA-Z],,g" $(BUILD_DIR)/$(OUT_FILES_DIR)/$(BUILD_DIR)_$(FILENAME).out \
	  > $(BUILD_DIR)/$(OUT_FILES_DIR)/$(BUILD_DIR)_$(FILENAME).out.tmp \
	  && mv $(BUILD_DIR)/$(OUT_FILES_DIR)/$(BUILD_DIR)_$(FILENAME).out.tmp $(BUILD_DIR)/$(OUT_FILES_DIR)/$(BUILD_DIR)_$(FILENAME).out
	@sed -n -i '/Waiting/!p' $(BUILD_DIR)/$(OUT_FILES_DIR)/$(BUILD_DIR)_$(FILENAME).out
	@mv $(BUILD_DIR)/jpeg_compsoc/binary.pnm $(BUILD_DIR)/$(OUT_PICTURES_DIR)/$(FILENAME).pnm 

compare_pictures: $(OUT_PICTURES)
$(OUT_PICTURES):
	@$(eval BUILD_DIR_LENGTH := $(shell printf $(BUILD_DIR) | wc -m))
	@$(eval TO_CUT := $(shell expr $(BUILD_DIR_LENGTH) + 18))
	@$(eval FILENAME := $(shell echo $@ | cut -c $(TO_CUT)- | rev | cut -c 5- | rev | tr -d '\n')) 
	@$(eval DIFF_RESULT := $(shell diff benchmark_files/binary_pictures/$(FILENAME).pnm $@ )) 
	@# benchmark_files/binary_pictures/$(FILENAME).pnm $@

	@printf "\e[1;91m"
	@printf $(FILENAME)": "
	@printf "\e[0m"
	@if [ "$(DIFF_RESULT)" = "" ]; then printf "OK."; else printf "Differs."; fi 
	@printf "\n"

interpret_clean:
	@rm -f $(RESULTS_DIR)/results_$(BUILD_DIR).txt
	@touch $(RESULTS_DIR)/results_$(BUILD_DIR).txt

interpret_results: interpret_clean $(OUT_FILES) interpret_results_msg
$(OUT_FILES):
	$(eval BUILD_DIR_LENGTH := $(shell printf $(BUILD_DIR) | wc -m))
	$(eval TO_CUT := $(shell expr $(BUILD_DIR_LENGTH) + 20))
	$(eval FILENAME := $(shell echo $@ | cut -c $(TO_CUT)- | rev | cut -c 1- | rev | tr -d '\n')) 
	@printf "\e[1;94m"
	@printf $(FILENAME) | tee -a $(RESULTS_DIR)/results_$(BUILD_DIR).txt
	@printf ": " >> $(RESULTS_DIR)/results_$(BUILD_DIR).txt
	@printf "\e[0m"
	@printf "\n" | tee -a $(RESULTS_DIR)/results_$(BUILD_DIR).txt
	@mkdir -p $(RESULTS_DIR)
	@python benchmark_files/interpret.py $@ >> $(RESULTS_DIR)/results_$(BUILD_DIR).txt
	@printf "\n" >> $(RESULTS_DIR)/results_$(BUILD_DIR).txt

interpret_results_msg:
	@printf "Results are saved in file '$(RESULTS_DIR)/results_$(BUILD_DIR).txt'.\n"

tail:
	$(eval OUT_FILE := $(shell find . -maxdepth 3 -name "*.out" -type f -printf '%T@ %p\n' | sort -n | tail -1 | cut -f2- -d" " ))
	@printf "\e[1;32m"
	@printf $(OUT_FILE)": \n"
	@printf "\e[0m"
	@tail -f $(OUT_FILE)

diff:
	@meld <(hexdump -C benchmark_files/bin_pics/$(SELECT).pnm) <(hexdump -C $(BUILD_DIR)/$(OUT_PICTURES_DIR)/$(SELECT).pnm)

all: $(BUILD_DIRS)
$(BUILD_DIRS):
	$(eval DIRNAME := $(shell echo $@ | rev | cut -c 3- | rev | tr -d '\n'))
	@printf "\e[1;97m"
	@echo $(DIRNAME)":"
	@printf "\e[0m"
ifeq ($(CMD),)
	@$(MAKE) -C $(MAKEFILE_DIR) BUILD_DIR=${DIRNAME} SELECT=$(SELECT) VERBOSE=$(VERBOSE)
else ifeq ($(CMD),generate)
	@$(MAKE) -C $(MAKEFILE_DIR) generate_pictures BUILD_DIR=${DIRNAME} SELECT=$(SELECT) VERBOSE=$(VERBOSE)
else ifeq ($(CMD), interpret)
	@$(MAKE) -C $(MAKEFILE_DIR) interpret_results BUILD_DIR=$@ SELECT=$(SELECT) VERBOSE=$(VERBOSE)
else ifeq ($(CMD), compare)
	@$(MAKE) -C $(MAKEFILE_DIR) compare_pictures BUILD_DIR=$@ SELECT=$(SELECT) VERBOSE=$(VERBOSE)
endif

both:
	@#python3 switch.py to-1
	@#$(MAKE) -C $(MAKEFILE_DIR) all BUILD_DIRS=_dp_simple_mmio_4_cores/.

	@# initial_code/.\ _initial_code_1_core/.\ _dp_simple_dma_1_core/.\ _dp_pingpong_v2_dma_4_cores/.\ _hybrid_v1_mb1+3_mb2+4/.\ _hybrid_v2_mb3_mb1+2_mb4/.\ _dp_simple_mmio_2_cores/.\ 
	@# _dp_prefetch_dma_1_core (DONE)
	@# _dp_pingpong_v2_dma_1_core (DONE)
	@# _fp_v2_mb3ToMb1 (DONE)
	@# _fp_v3_3cores (DONE)
	@# _dp_pingpong_v3_dma_1_core (DONE)
	@# _dp_pingpong_v1_dma_4_cores (DONE)
	@# _fp_v1_mb1ToMb2 (DONE)
	@# _dp_pingpong_v3_dma_4_cores (DONE)
	@# _dp_pingpong_v1_dma_1_core (DONE)
	@# _dp_rowread_dma_1_core (DONE)

	python3 switch.py to-3
	@$(MAKE) -C $(MAKEFILE_DIR) all BUILD_DIRS=_dp_pingpong_v1_dma_1_core/.#\ _dp_pingpong_v1_dma_4_cores/.\ _dp_rowread_dma_1_core/.\ _dp_pingpong_v2_dma_4_cores/.\ _dp_pingpong_v3_dma_1_core/.\ _dp_pingpong_v3_dma_4_cores/.\ _dp_prefetch_dma_1_core/.\ _dp_rowread_dma_1_core/.\ _dp_simple_dma_1_core/.\

	@# _dp_pingpong_v2_dma_1_core (DONE)
	@# _dp_pingpong_v1_dma_4_cores (DONE)
	@# _dp_pingpong_v2_dma_4_cores (DONE)
	@# _dp_simple_dma_1_core (DONE)
	@# _dp_pingpong_v1_dma_1_core (DONE)
	@# _dp_simple_mmio_2_cores (DONE)
	@# _dp_simple_mmio_4_cores (DONE)

