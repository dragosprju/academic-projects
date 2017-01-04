#ifndef constants_hpp
#define constants_hpp

#define DEBUG 0
#define DEBUG_ECHO 0

#define STARTUP_WAIT 1000
#define WHILE_WAITS 100

#define LED_BLINK 200 // in microseconds, referring to red led blinking when being bent
#define LEDS_TURNOFF_AFTER 30 // arbitrary ticks, referring to threshold change led turn on's and off's
#define BTNS_ACKNOWLEDGED_AFTER 10

#define TOUCH_SAFETY_VALUE 0.07
#define THRESHOLD_FALLBACK 0.15
#define THRESHOLD_MAX 0.4

#define BENT_WAIT_FALLBACK 5000
#define STRAIGHT_WAIT_FALLBACK 3000

#define BLUETOOTH_ENABLE_WAIT 5000

#endif