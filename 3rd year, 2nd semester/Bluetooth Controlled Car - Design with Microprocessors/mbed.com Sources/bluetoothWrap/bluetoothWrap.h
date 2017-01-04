#ifndef BLUETOOTH_WRAP
#define BLUETOOTH_WRAP

#include "mbed.h"
#include "rtos.h"

#include "motorsWrap.h"
#include "sensorWrap.h"

class BluetoothWrap {
private:
    static Serial bt;
    static char c;
    
    static MotorsWrap mw;
    static SensorWrap sw;
    
    static Leds leds;
public:
    static void run(void const* args);
};
#endif