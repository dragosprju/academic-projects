#include "mbed.h"
#include "sensorWrap.h"
#include "sensor.h"
#include "settings.h"

Mutex SensorWrap::ABMutex = Mutex();
Mutex SensorWrap::RepMutex = Mutex();

bool SensorWrap::autobreak = false;
bool SensorWrap::report = false;

void SensorWrap::toggleAutobreak() {
    bool ab;
    ABMutex.lock();
    autobreak ^= 1;
    ab = autobreak;
    ABMutex.unlock();
    
    if (ab) {        
        sensor.bt.printf("Autobreak enabled!\r\n");
    }
    else {
        sensor.bt.printf("Autobreak disabled!\r\n");
    }
}
    
void SensorWrap::toggleReporting() {
    bool rep;
    
    RepMutex.lock();
    report ^= 1;
    rep = report;
    RepMutex.unlock();
    
    if (rep) {        
        sensor.bt.printf("Starting to report distance.\r\n");
    }
    else {
        sensor.bt.printf("Stopping distance reporting.\r\n");
    }
}

void SensorWrap::reporting(void const* args) {
    while (1) {
        RepMutex.lock();
        if (report) {
            RepMutex.unlock();        
            sensor.reportDistance();
        }
        else {
            RepMutex.unlock();
        }
        wait_ms(SENSOR_REPORT_DELAY_MS);
    }
}

void SensorWrap::run(void const* args) {
    while (1) {
        bool ab;
        ABMutex.lock();        
        ab = autobreak;
        ABMutex.unlock();
        
        RepMutex.lock();
        if (autobreak|| report) {
            RepMutex.unlock();
            sensor.updateDistance();
        }
        else {
            RepMutex.unlock();
        }
        
        if (ab) {
            sensor.autobreakBrain();
        }
    }
}