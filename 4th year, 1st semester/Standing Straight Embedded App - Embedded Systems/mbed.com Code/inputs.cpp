#include "mbed.h"
#include "rtos.h"
#include "TSISensor.h"
#include "constants.hpp"

// ----------------------------------

// Private

    TSISensor tsi;
    float threshold_value = THRESHOLD_FALLBACK;
    
    DigitalIn button_one(PTC11);
    DigitalIn button_two(PTC10);
    
// ----------------------------------

// External

    // Variables
    extern PwmOut rled;
    extern PwmOut gled;
    extern DigitalOut bluetooth_enable;
    
    // Functions
    extern void get_reference();
    extern void blink_green_twice();

// ----------------------------------

/***************/
/* INPUTS MAIN */
/***************/
void inputs_main() {
    float previousRead;
    float currentRead;
    int counter = 0;
    
    Thread::wait(STARTUP_WAIT);
    
    while (1) {
        
        // TOUCH SENSOR READING
        currentRead = tsi.readPercentage();
         
        if (currentRead >= TOUCH_SAFETY_VALUE && currentRead != previousRead) {
            rled = 1.0 - currentRead;
            gled = 0.0 + currentRead;
            
            counter = 0;
            
            previousRead = currentRead;
        }
        else {            
            if (counter >= LEDS_TURNOFF_AFTER) {
                rled = 1.0;
                gled = 1.0;
                
                if (previousRead >= TOUCH_SAFETY_VALUE) {                
                    threshold_value = previousRead * THRESHOLD_MAX;
                }        
            }
            else if (counter < LEDS_TURNOFF_AFTER) {
                counter++;
            }
        }
        
        // LEFTMOST BUTTON READING
        if (button_one) {
            blink_green_twice();
            get_reference();
        }
        
        // RIGHTMOST BUTTON READING
        if (button_two) {
            ;
        }  
        
        Thread::wait(WHILE_WAITS);
    }
}
