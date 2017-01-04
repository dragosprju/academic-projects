#include "mbed.h"
#include "rtos.h"
#include "constants.hpp"

// Thread mains
extern void inputs_main();
extern void accelerometer_main();
extern void bluetooth_main();

// Extern functions
extern void disable_leds();

int main() {
    Thread inputs_thread;
    Thread bluetooth_thread;
    Thread accelerometer_thread;
    
    disable_leds();
    
    inputs_thread.start(&inputs_main);
    bluetooth_thread.start(&bluetooth_main);
    accelerometer_thread.start(&accelerometer_main);
    
    inputs_thread.join();
    bluetooth_thread.join();
    accelerometer_thread.join();
}

