#include "mbed.h"
#include "rtos.h"
#include "constants.hpp"

// -----------------------------------------

// Owned variables

    // That I declare and use only in here
    PwmOut rled(LED_RED);
    PwmOut gled(LED_GREEN);
    PwmOut bled(LED_BLUE);
    
    void interm_red_main(void const *n);
    RtosTimer interm_red_timer(interm_red_main);

    // That others set for me

    // That I set for others to read
    
// -----------------------------------------

// External variables


// -----------------------------------------

/******************/
/* Functions only */
/******************/

void disable_leds() {
    rled = 1.0;
    bled = 1.0;
    gled = 1.0;
}

void interm_red_main(void const *n) {
    rled = !rled;
}    

void interm_red_start() {
    interm_red_timer.start(LED_BLINK);
}

void interm_red_stop() {
    interm_red_timer.stop();
}

void blink_green_twice() {
    gled = 0.0;
    wait_ms(LED_BLINK);
    gled = 1.0;
    wait_ms(LED_BLINK);
    gled = 0.0;
    wait_ms(LED_BLINK);
    gled = 1.0;
}  
    
    