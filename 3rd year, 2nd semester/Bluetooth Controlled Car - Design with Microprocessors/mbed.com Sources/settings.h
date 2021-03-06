#define PIN_MOTOR_L_FW PTE23
#define PIN_MOTOR_L_BW PTE22
#define PIN_MOTOR_R_FW PTE21
#define PIN_MOTOR_R_BW PTE20

#define BT_BAUDRATE 9600
#define BT_TX PTA2
#define BT_RX PTA1

#define PIN_SENSOR_TRIG PTA4
#define PIN_SENSOR_ECHO PTA5

#define PERIOD 0.02
#define SPEEDS {-0.02, -0.016, -0.012, -0.0075, 0.0, 0.0075, 0.012, 0.016, 0.02}
#define FIRST_INDEX 4
#define LAST_INDEX 8

#define DISTANCES_TO_STOP {15, 15, 15, 15, 15, 25, 30, 35, 40}
#define SENSOR_REPORT_DELAY_MS 2000
#define SENSOR_ERROR 10