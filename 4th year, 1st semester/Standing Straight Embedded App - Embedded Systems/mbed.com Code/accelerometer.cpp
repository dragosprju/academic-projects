#include "mbed.h"
#include "rtos.h"
#include "constants.hpp"
#include "MMA8451Q.h"

// -----------------------------------------

// Private

    PinName const SDA = PTE25;
    PinName const SCL = PTE24;
    #define MMA8451_I2C_ADDRESS (0x1d<<1)
    MMA8451Q acc(SDA, SCL, MMA8451_I2C_ADDRESS);
    
    int bent_for_some_time = 0; // 0 = Straight; 1 = Bent.

    int bent_check_time = BENT_WAIT_FALLBACK;
    int straight_check_time = STRAIGHT_WAIT_FALLBACK;
    
    float init_x, init_y, init_z;
    float curr_x, curr_y, curr_z;
    float diff_x, diff_y, diff_z;
    
// -----------------------------------------

// External

    // Variables
    extern float threshold_value;
    
    // Functions
    extern void interm_red_start();
    extern void interm_red_stop();


// -----------------------------------------

void get_reference() {
  
    init_x = acc.getAccX();
    init_y = acc.getAccY();
    init_z = acc.getAccZ();
  
}

/**********************/
/* ACCELEROMETER MAIN */
/**********************/
void accelerometer_main() {
    
    Thread::wait(STARTUP_WAIT);
    
    get_reference();
    
    int wait_till_bent_announce = bent_check_time / WHILE_WAITS;
    int wait_till_straight_announce = straight_check_time / WHILE_WAITS;
    
    int bent_counter = 0;
    int straight_counter = 0;
    
    while (1) {
        curr_x = acc.getAccX();
        curr_y = acc.getAccY();
        curr_z = acc.getAccZ();
        
        diff_x = curr_x - init_x;
        diff_y = curr_y - init_y;
        diff_z = curr_z - init_z;
        
        if (abs(diff_x) > threshold_value || abs(diff_y) > threshold_value || abs(diff_z) > threshold_value) {
            if (bent_counter >= wait_till_bent_announce) {
                if (!bent_for_some_time) interm_red_start();
                bent_for_some_time = 1;
            }
            else {
                bent_counter++;
                straight_counter = 0;
            }
        }
        else {
            if (straight_counter >= wait_till_straight_announce) {
                bent_for_some_time = 0;
                interm_red_stop();
            }
            else {
                straight_counter++;
                bent_counter = 0;
            }    
        }  
         
        Thread::wait(WHILE_WAITS);
    }
    
    
}