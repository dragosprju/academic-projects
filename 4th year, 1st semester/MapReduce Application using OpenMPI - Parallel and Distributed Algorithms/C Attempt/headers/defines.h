#ifndef configuration_h
#define configuration_h

// Colors
#define KNRM  "\x1B[0m"
#define KBLD  "\x1B[1m"
#define KRED  "\x1B[31m" 
#define KGRN  "\x1B[92m" // Tema mea de terminal cere alt tip de verde
#define KYEL  "\x1B[33m"
#define KBLU  "\x1B[34m"
#define KMAG  "\x1B[35m"
#define KCYN  "\x1B[36m"
#define KWHT  "\x1B[37m"

// General working configs
#define ROOT 2
#define MAX_FILEPATH_LEN 256
#define WORKDIR "working_files/"

// MPI Send/Receive constants
#define TAG 0
#define TIMEOUT_SENDER 15
#define TIMEOUT_RECEIVER 15
#define TIMEOUT_OCCUPANCY 1

// Debug prints
#define FORGOTTEN_POINTERS_PRINT 1

#define EXTRA_PRINTS_PHASE_0_1 0
#define EXTRA_PRINTS_PHASE_0_2 0
#define EXTRA_PRINTS_PHASE_0_3 0
#define EXTRA_PRINTS_PHASE_0_4 0

#define EXTRA_PRINTS_PHASE_1_1 0
#define EXTRA_PRINTS_PHASE_1_2 0
#define EXTRA_PRINTS_PHASE_1_3 0
#define EXTRA_PRINTS_PHASE_1_4 1

#define EXTRA_PRINTS_PHASE_2_1 0
#define EXTRA_PRINTS_PHASE_2_2 0
#define EXTRA_PRINTS_PHASE_2_3 0

#define EXTRA_PRINTS_PHASE_3_1 1

// File splitting
#define OF_N_CHARS 5000

#endif
