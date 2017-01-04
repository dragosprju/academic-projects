#ifndef LEDS2
#define LEDS2 

#include "mbed.h"

class Leds {
private:
    static DigitalOut red_led;
    static DigitalOut green_led;
    static DigitalOut blue_led;
    
public:    
    static void setRed() {
        red_led = 0;
    }
    
    static void setGreen() {
        green_led = 0;
    }
    
    static void setBlue() {
        blue_led = 0;
    }
    
    static void clearRed() {
        red_led = 1;
    }
    
    static void clearGreen() {
        green_led = 1;
    }
    
    static void clearBlue() {
        blue_led = 1;
    }
    
    static void clearAll() {
        red_led = 1;
        green_led = 1;
        blue_led = 1;
    }
};

#endif