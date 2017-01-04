#ifndef MOTORS_WRAP
#define MOTORS_WRAP

#include "mbed.h"
#include "rtos.h"
#include "leds.h"
#include "motors.h"

enum MotorsCommand {
    Hold,
    Accelerate,
    Decelerate,
    Brake,
    TiltLeft,
    TiltRight,
    RotateLeft,
    RotateRight
};

class MotorsWrap {
private:    
    static Motors motors;
    static Leds leds;
    
    static MemoryPool<MotorsCommand, 16> cmdMPool;
    static Queue<MotorsCommand, 16> cmdQueue;
    static MotorsCommand cmd;
    static Mutex cmdMutex;
    
    static MotorsCommand getCmd();
public:
    static void putCmd(MotorsCommand cmd);
    static void run(void const* args);
};



#endif
