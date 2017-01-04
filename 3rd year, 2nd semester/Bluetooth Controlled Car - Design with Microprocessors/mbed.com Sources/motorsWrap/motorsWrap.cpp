#include "mbed.h"

#include "motorsWrap.h"
#include "leds.h"
#include "motors.h"

MotorsCommand MotorsWrap::cmd = Hold;
MemoryPool<MotorsCommand, 16> MotorsWrap::cmdMPool = MemoryPool<MotorsCommand, 16>();
Queue<MotorsCommand, 16> MotorsWrap::cmdQueue = Queue<MotorsCommand, 16>();
Mutex MotorsWrap::cmdMutex = Mutex();

void MotorsWrap::putCmd(MotorsCommand cmd) {    
    MotorsCommand *newCmd = cmdMPool.alloc();
    *newCmd = cmd;
    cmdMutex.lock();
    cmdQueue.put(newCmd);
    cmdMutex.unlock();
}

MotorsCommand MotorsWrap::getCmd() {
    MotorsCommand toReturn;
    osEvent evt = cmdQueue.get();
    if (evt.status == osEventMessage) {
        MotorsCommand *cmd = (MotorsCommand*)evt.value.p;
        toReturn = *cmd;
        cmdMPool.free(cmd);
    }
    return toReturn;
}

void MotorsWrap::run(void const* args) {   
    while(true) {
        cmd = getCmd();
        switch(cmd) {
            case Hold:
                break;
            case Accelerate:
                motors.accelerate();
                break;
            case Decelerate:
                motors.decelerate();
                break;
            case RotateLeft:
                motors.alternateLeft();
                break;
            case RotateRight:
                motors.alternateRight();
                break;
            case TiltLeft:
                motors.tiltLeft();
                break;
            case TiltRight:
                motors.tiltRight();
                break;
            case Brake:
                motors.brake();
                break;  
        }
    }
    
}