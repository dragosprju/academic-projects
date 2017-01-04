#include "mbed.h"
#include "rtos.h"

#include "leds.h"
#include "motors.h"
#include "motorsWrap.h"
#include "bluetoothWrap.h"
#include "sensorWrap.h"
#include "settings.h"

int main () {       
    MotorsWrap mw;
    BluetoothWrap bw;
    SensorWrap sw;
    Leds leds;
    
    leds.clearAll();
    Thread bluetoothThread(bw.run);
    Thread motorsThread(mw.run);    
    Thread sensorReportingThread(sw.reporting);
    Thread sensorThread(sw.run);
    while(true);
}