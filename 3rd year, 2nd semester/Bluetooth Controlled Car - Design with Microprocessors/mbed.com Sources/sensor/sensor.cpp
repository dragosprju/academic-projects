#include "mbed.h"
#include "rtos.h"
#include "sensor.h"
#include "settings.h"

Serial Sensor::bt(BT_TX, BT_RX);
HCSR04 Sensor::sensor = HCSR04(PIN_SENSOR_ECHO, PIN_SENSOR_TRIG);
unsigned int Sensor::stopAt[] = DISTANCES_TO_STOP;
float Sensor::distance = 20;
Mutex Sensor::distanceMutex = Mutex();
float Sensor::last3Distances[3] = {0.0, 0.0, 0.0};

void Sensor::updateDistance() {
    distanceMutex.lock();
    distance = sensor.getDistance_cm();
    distanceMutex.unlock();
}   

float Sensor::getDistance() {
    distanceMutex.lock();
    float toRet = distance;
    distanceMutex.unlock();
    return toRet;    
}

void Sensor::autobreakBrain() {
    int speed;
    bool gottaStop = false;
    motors.getSpeed(speed);
      
    distanceMutex.lock();
    if (distance != 2.00 || distance != 400.00) {        
        last3Distances[0] = last3Distances[1];
        last3Distances[1] = last3Distances[2];  
        last3Distances[2] = distance;
        distanceMutex.unlock();
    }
    else {
        leds.clearAll();
        leds.setRed();
        leds.setGreen();
    }  
    
    float error = SENSOR_ERROR * 2;
    if (last3Distances[0] > last3Distances[1]) {
        if (last3Distances[0] > last3Distances[2]) {
            error = last3Distances[0] - last3Distances[2];
        }
        else {
            error = last3Distances[2] - last3Distances[0];
        }
    }
    else {
        if (last3Distances[1] > last3Distances[2]) {
            error = last3Distances[1] - last3Distances[2];
        }
        else {
            error = last3Distances[2] - last3Distances[1];
        }
    }                   
    if (error <= SENSOR_ERROR && last3Distances[2] <= stopAt[speed]) {
        gottaStop = true;
        leds.clearGreen();
        leds.setRed(); 
    }
    else {
        distanceMutex.unlock();    
        leds.clearRed();
        leds.setGreen();
    }
    
    Direction dir = motors.getDirection();
    if (gottaStop) {
        switch(dir) {
            case Forward:
            case ForwardTiltedLeft:
            case ForwardTiltedRight:
            case OnlyLeftForward:
            case OnlyRightForward:
            case Unknown:
                mw.putCmd(Brake);
                break;
        }
    }
}