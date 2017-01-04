#ifndef SENSOR
#define SENSOR

#include "mbed.h"
#include "rtos.h"
#include "HCSR04.h"
#include "motors.h"
#include "motorsWrap.h"
#include "leds.h"

class Sensor {
private:     
    static HCSR04 sensor;
    static Motors motors;
    static MotorsWrap mw;
    static Leds leds;
    static unsigned int stopAt[];
    static Mutex distanceMutex;
    
    static float distance;
    static float last3Distances[3];
public: 
    static Serial bt;
    
    static void updateDistance();
    static float getDistance();
    
    static void reportDistance() {
        bt.printf("Distance: %.2f\r\n", getDistance());
    }
    static void autobreakBrain();
};

#endif