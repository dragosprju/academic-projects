#ifndef MOTORS
#define MOTORS

#include "rtos.h"
#include "DRV8833.h"
#include "settings.h"

enum Direction {
    Stop,
    Forward,
    Backward,
    ForwardTiltedLeft,
    ForwardTiltedRight,
    BackwardTiltedLeft,
    BackwardTiltedRight,
    OnlyLeftForward,
    OnlyRightForward,
    OnlyLeftBackward,
    OnlyRightBackward,
    AlternatingLeft,
    AlternatingRight,
    Unknown
};

class Motors {
private:
    static DRV8833 motorL;
    static DRV8833 motorR;
    
    static const float speeds[];
    
    static int speedLeft;
    static int speedRight;
    
    static Mutex speedMutex;
    
    static void setMotors() {
        motorL.speed2(speeds[speedLeft]);
        motorR.speed2(speeds[speedRight]);
    }
    
    static void incLeft() {
        if (speedLeft < LAST_INDEX) {
            speedLeft++;
        }
    }
    
    static void incRight() {
        if (speedRight < LAST_INDEX) {
            speedRight++;
        }
    }
    
    static void decLeft() {
        if (speedLeft > 0) {
            speedLeft--;
        }
    }
    
    static void decRight() {
        if (speedRight > 0) {
            speedRight--;
        }
    }
  
    static bool equalizeMotors(bool forward);                
    
public:        
    static void accelerate();    
    static void decelerate();
    
    static void brake() {
        speedLeft = FIRST_INDEX;
        speedRight = FIRST_INDEX;
        setMotors();
    }
    
    static void alternateLeft();    
    static void alternateRight(); 
    static void tiltLeft();    
    static void tiltRight();
    
    static int getSpeed();
    static void getSpeed(int &speed);
    
    static Direction getDirection();  
};

#endif
        
    