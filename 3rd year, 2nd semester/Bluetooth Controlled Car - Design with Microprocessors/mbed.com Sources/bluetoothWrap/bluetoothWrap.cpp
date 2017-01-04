#include "mbed.h"
#include "rtos.h"
#include "bluetoothWrap.h"
#include "motorsWrap.h"
#include "settings.h"

char BluetoothWrap::c = '\0';
Serial BluetoothWrap::bt(BT_TX, BT_RX);
Leds BluetoothWrap::leds;

void BluetoothWrap::run(void const* args) {  
    bt.baud(BT_BAUDRATE);
    char c;
    while(true) {
        c = bt.getc();
        switch(c) {
            case 'w':
                mw.putCmd(Accelerate);
                break;
            case 's':
                mw.putCmd(Decelerate);
                break;
            case 'q':
                mw.putCmd(TiltLeft);
                break;
            case 'e':
                mw.putCmd(TiltRight);
                break;
            case 'a':
                mw.putCmd(RotateLeft);
                break;
            case 'd':
                mw.putCmd(RotateRight);
                break;
            case 'x':
                mw.putCmd(Brake);
                break;
            case 'c':
                sw.toggleAutobreak();
                break;
            case '5':
                sw.toggleReporting();
                break;
        }
        c = '\0';
    }
    
}