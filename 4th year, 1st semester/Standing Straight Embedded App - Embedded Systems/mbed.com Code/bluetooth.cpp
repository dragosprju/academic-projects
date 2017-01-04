#include "mbed.h"
#include "rtos.h"
#include "MODSERIAL.h"
#include "constants.hpp"

// -----------------------------------------

// Private

    DigitalOut bluetooth_enable(PTC4);
    Serial bt(PTE0, PTE1);
    struct tm t;
    
    char bluetooth_command = 'x';
    
    float known_threshold_value = THRESHOLD_FALLBACK;
    int known_bent_value = 0;
    
// -----------------------------------------

// External

    // Variables
    extern float threshold_value;
    extern int bent_for_some_time;
    
    extern float curr_x, curr_y, curr_z;
    extern float diff_x, diff_y, diff_z;

    // Functions


// -----------------------------------------

// Functions

void get_str(Serial &src, char* where_to_write, int len, int end_properly = 0) {
    int i;
    for (i = 0; i < len; i++) {
        where_to_write[i] = src.getc();
        if (DEBUG_ECHO) src.putc(where_to_write[i]);
    } 
    
    if (end_properly) {
        where_to_write[i] = '\0';
    }
}

inline void print_time() {
    time_t seconds = time(NULL);
    char buffer[20];
    strftime(buffer, 20, "%Y-%m-%d %H:%M:%S\0", localtime(&seconds));
    bt.printf("%s", buffer);
}

inline void dot_newline() {
    //Thread::wait(50);
    bt.putc('.');
    //Thread::wait(50);
    bt.putc('\r');
    bt.putc('\n');
}

inline void newline() {
    //Thread::wait(50);
    bt.putc('\r');
    bt.putc('\n');
}
/******************/
/* BLUETOOTH MAIN */
/******************/
void bluetooth_main() {
    char timestr[20]; // YYYY-MM-DD HH:MM:SS equals 19 chars
    
    bluetooth_enable = 1;
    bt.baud(115200);
    
    //int time_set = 0;    
    while (1) {
        if (!bluetooth_enable) {
            Thread::wait(BLUETOOTH_ENABLE_WAIT);
            continue;
        }

        if (bt.readable()) {
            bluetooth_command = bt.getc();
        }
        else {
            bluetooth_command = 'x';
        }
        
        switch(bluetooth_command) {
            case 's': // Gotta find out what time it is
                bt.printf("Awaiting time update"); dot_newline();
                get_str(bt, timestr, 19, 1);
                sscanf(timestr, "%d-%d-%d %d:%d:%d", &t.tm_year, &t.tm_mon, &t.tm_mday,
                                                        &t.tm_hour, &t.tm_min, &t.tm_sec);
                                                        
                t.tm_year = t.tm_year - 1900;
                t.tm_mon = t.tm_mon - 1;
                
                set_time(mktime(&t));
                
                bt.printf("Time updated to: ");
                print_time();
                dot_newline();
                break;
            
            case 't':
                bt.printf("Time is: ");
                print_time();
                dot_newline();             
                        
                break;
                
            case 'h':
                bt.printf("Threshold value is %1.2f", threshold_value);
                dot_newline();
                
                break;
        }
        
        // Checks to inform automatically
        if (known_threshold_value != threshold_value) {
            bt.printf("Threshold value was manually set to %1.2f", threshold_value); dot_newline();
            known_threshold_value = threshold_value;
        }
        
        if (known_bent_value != bent_for_some_time) {
            if (bent_for_some_time == 1) {
                bt.printf("Detected bent as of ");
                print_time();
                dot_newline();
                
                if (DEBUG) {
                    bt.printf("Accelerometer values (x,y,z): "); newline();
                    bt.printf("- Actual: %1.2f, %1.2f, %1.2f", curr_x, curr_y, curr_z); newline();
                    bt.printf("- Difference: %1.2f, %1.2f, %1.2f", diff_x, diff_y, diff_z); newline();
                }
            }
            else {
                bt.printf("Detected straight as of ");
                print_time();
                dot_newline();
            }
            known_bent_value = bent_for_some_time;
        }
        
        Thread::wait(WHILE_WAITS);
    }    
}