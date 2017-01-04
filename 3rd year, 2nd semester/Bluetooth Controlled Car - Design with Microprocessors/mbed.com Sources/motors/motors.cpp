#include "motors.h"
#include "settings.h"
#include "DRV8833.h"

DRV8833 Motors::motorL(PIN_MOTOR_L_FW, PIN_MOTOR_L_BW);
DRV8833 Motors::motorR(PIN_MOTOR_R_FW, PIN_MOTOR_R_BW);

int Motors::speedLeft = FIRST_INDEX;
int Motors::speedRight = FIRST_INDEX;

const float Motors::speeds[] = SPEEDS;

Mutex Motors::speedMutex = Mutex();

void Motors::accelerate() {
    bool canAccelerate = equalizeMotors(true);
    if (canAccelerate) {
        incLeft();
        incRight();
    }
    setMotors();
}

void Motors::decelerate() {
    bool canDecelerate = equalizeMotors(false);
    if (canDecelerate) {
        decLeft();
        decRight();
    }
    setMotors();
}

void Motors::alternateLeft() {
    decLeft();
    incRight();        
    setMotors();       
}

void Motors::alternateRight() {
    incLeft();
    decRight();
    setMotors();
}

void Motors::tiltLeft() {
    incRight();
    setMotors();
}

void Motors::tiltRight() {
    incLeft();
    setMotors();
}

bool Motors::equalizeMotors(bool forward) {
    if (speedLeft == speedRight) {
        return true;
    }
    
    Direction dir = getDirection();
    
    if (dir == AlternatingLeft) {
        if (forward == true) {
            speedLeft = speedRight;
        }
        else {
            speedRight = speedLeft;
        }
    }
    else if (dir == AlternatingRight) {
        if (forward == true) {
            speedRight = speedLeft;
        }
        else {
            speedLeft = speedRight;
        }
    }
    else if (dir == OnlyLeftForward || dir == OnlyLeftBackward) {
        if (OnlyLeftForward && forward) {
            speedLeft = speedRight;
        }
        else if (OnlyLeftBackward && !forward) {
            speedLeft = speedRight;
        }
        else {
            brake();
        }
    }
    else if (dir == OnlyRightForward || dir == OnlyRightBackward) {
        if (OnlyRightForward && forward) {
            speedRight = speedLeft;
        }
        else if (OnlyRightBackward && !forward) {
            speedRight = speedLeft;
        }
        else {
            brake();
        }
    }
    else if (speedLeft < FIRST_INDEX && speedRight < FIRST_INDEX) {
        // If we're going backwards
        if (speedLeft < speedRight) {
            speedLeft = speedRight;
        }
        else {
            speedRight = speedLeft;
        }
    }
    else if (speedLeft >= FIRST_INDEX && speedRight >= FIRST_INDEX) {
        // If we're going forwards
        if (speedLeft < speedRight) {
            speedRight = speedLeft;
        }
        else {
            speedLeft = speedRight;
        }
    }
    return false;  
}

Direction Motors::getDirection() {
    if (speedLeft == FIRST_INDEX && speedRight == FIRST_INDEX) {
        return Stop;
    }
    else if (speedLeft > FIRST_INDEX && speedRight > FIRST_INDEX) {
        if (speedLeft > speedRight) {
            return ForwardTiltedLeft;
        }
        else if (speedLeft < speedRight) {
            return ForwardTiltedRight;
        }
        else {
            return Forward;
        }
    }
    else if (speedLeft < FIRST_INDEX && speedRight < FIRST_INDEX) {
        if (speedLeft < speedRight) {
            return BackwardTiltedLeft;
        }
        else if (speedLeft < speedRight) {
            return BackwardTiltedRight;
        }
        else {
            return Backward;
        }
    }
    else if (speedLeft > FIRST_INDEX && speedRight == FIRST_INDEX) {
        return OnlyRightForward;
    }
    else if (speedLeft == FIRST_INDEX && speedRight > FIRST_INDEX) {
        return OnlyLeftForward;
    }
    else if (speedLeft < FIRST_INDEX && speedRight == FIRST_INDEX) {
        return OnlyRightBackward;
    }
    else if (speedLeft == FIRST_INDEX && speedRight < FIRST_INDEX) {
        return OnlyLeftBackward;
    }
    else if (speedLeft < FIRST_INDEX && speedRight > FIRST_INDEX) {
        return AlternatingLeft;
    }
    else if (speedLeft > FIRST_INDEX && speedRight < FIRST_INDEX) {
        return AlternatingRight;
    }
    else {
        return Unknown;
    }
}

int Motors::getSpeed() {
    if (speedLeft < FIRST_INDEX && speedRight < FIRST_INDEX) {
        if (speedLeft < speedRight) {
            return speedLeft;
        }
        else {
            return speedRight;
        }
    }
    else {
        if (speedLeft < speedRight) {
            return speedRight;
        }
        else {
            return speedLeft;
        }
    }
}

void Motors::getSpeed(int &speed) {
    speedMutex.lock();
    speed = getSpeed();
    speedMutex.unlock();
}   