#ifndef SENSOR_WRAP
#define SENSOR_WRAP

#include "sensor.h"
#include "motorsWrap.h"
#include "settings.h"

class SensorWrap {
private:
    static Sensor sensor;
    static MotorsWrap mw;
    static Leds leds;
    
    static bool autobreak;
    static bool report;
    
    static Mutex ABMutex;
    static Mutex RepMutex;
public:
    static void reporting(void const* args);
    static void run(void const* args);
    
    static void toggleAutobreak();
    static void toggleReporting();
};

#endif
