#include "mbed.h"
#include "TSISensor.h"
#include "BufferedSerial.h"

#define SENDER
#define RECEIVER
#define SPEED 50
#define BUFSIZE 32
#define PUBLISHSIZE 11

PwmOut red_led(LED_RED); 
PwmOut green_led(LED_GREEN);
PwmOut blue_led(LED_BLUE);

BufferedSerial usb(USBTX, USBRX);

TSISensor touch;

void usb_read(char *buffer, int noChars) {
    for (int i = 0; i < noChars; i++) {
        if (usb.readable()) {
            buffer[i] = usb.getc();
        }
    }
}

void usb_write(char *buffer, int noChars) {    
    for (int i = 0; i < noChars; i++) {
        usb.putc(buffer[i]);
    }
}

void CONNECT(int keep_alive_sec, char* _client_identifier) {
    // CONNECT = Type 1 on 4 bits + Reserved 0 on the other 4 bits
    char packet_type = 0x10;
    char remaining_length = 12 + strlen(_client_identifier);
    
    unsigned short int protocol_name_size = strlen("MQTT"); // 4 letters
    char protocol_name[] = "MQTT";
    
    char protocol_version = 4; // 4 = 3.1.1 current version
    
    // Username, Password, Retain for WILL, Retain of QoS (2), Flag of WILL, Clean Session, Reserved
    char connect_flags = 0x02;
    
    const short int client_identifier_size = strlen(_client_identifier);
    char* client_identifier = new char[client_identifier_size];
    for (int i = 0; i < client_identifier_size; i++) {
        client_identifier[i] = _client_identifier[i];
    }
    
    const unsigned int CONNECT_packet_size = 14 + client_identifier_size;
    char* CONNECT_packet = new char[CONNECT_packet_size];
    CONNECT_packet[0] = packet_type;
    CONNECT_packet[1] = remaining_length;
    CONNECT_packet[2] = (char)(protocol_name_size >> 8);
    CONNECT_packet[3] = (char)(protocol_name_size);
    CONNECT_packet[4] = protocol_name[0];
    CONNECT_packet[5] = protocol_name[1];
    CONNECT_packet[6] = protocol_name[2];
    CONNECT_packet[7] = protocol_name[3];
    CONNECT_packet[8] = (char)(protocol_version);
    CONNECT_packet[9] = connect_flags;
    CONNECT_packet[10] = (char)(keep_alive_sec >> 8);
    CONNECT_packet[11] = (char)(keep_alive_sec);
    CONNECT_packet[12] = (char)(client_identifier_size >> 8);
    CONNECT_packet[13] = (char)(client_identifier_size);
    for (int i = 14; i < 14 + client_identifier_size; i++) {
        CONNECT_packet[i] = client_identifier[i - 14];
    }
    
    // Sending
    usb_write(CONNECT_packet, CONNECT_packet_size);
    
    delete client_identifier;
    delete CONNECT_packet;
}

bool CONNACK() {
    bool connection_accepted = true;
    char CONNACK_ok[] = {0x20, 0x02, 0x00, 0x00};
    int noBytes = 4;
    char buffer[BUFSIZE];
    
    if (!usb.readable()) {
        return false;
    }
    else {
        usb_read(buffer, noBytes);
        for (int i = 0; i < noBytes; i++) {
            if (buffer[i] != CONNACK_ok[i]) {
                connection_accepted = false;
            }
        }
    }
    
    return connection_accepted;
}

void PUBLISH(char* topic, char* message) {
    // PUBLISH = Type 3 on 4 bits + Reserved 0 on the other 4 bits
    char packet_type = 0x30;
    char remaining_length = 2 + strlen(topic) + strlen(message);
    unsigned short int topic_length = strlen(topic);

    const unsigned int PUBLISH_packet_size = 4 + strlen(topic) + strlen(message);
    char* PUBLISH_packet = new char[PUBLISH_packet_size];
    PUBLISH_packet[0] = packet_type;
    PUBLISH_packet[1] = remaining_length;
    PUBLISH_packet[2] = (char)(topic_length >> 8);
    PUBLISH_packet[3] = (char)(topic_length);
    int i;
    for (i = 4; i < 4 + strlen(topic); i++) {
        PUBLISH_packet[i] = topic[i - 4];
    }
    for (int j = i; j < i + strlen(message); j++) {
        PUBLISH_packet[j] = message[j - i];
    }
    
    // Sending
    usb_write(PUBLISH_packet, PUBLISH_packet_size);    
    delete PUBLISH_packet;
}

unsigned short int SUBSCRIBE_packet_identifier = 0x0004;
void SUBSCRIBE(char* topic) {
    // SUBSCRIBE = Type 1 on 4 bits + 'Asking for acknowledge' flag
    char packet_type = 0x82;
    char remaining_length = 5 + strlen(topic);
    //unsigned short int packet_identifier = SUBSCRIBE_packet_identifier;
    unsigned short int topic_length = strlen(topic);
    char QoS = 0x00;
    
    const unsigned int SUBSCRIBE_packet_size = 7 + strlen(topic);
    char* SUBSCRIBE_packet = new char[SUBSCRIBE_packet_size];
    SUBSCRIBE_packet[0] = packet_type;
    SUBSCRIBE_packet[1] = remaining_length;
    SUBSCRIBE_packet[2] = (char)(SUBSCRIBE_packet_identifier >> 8);
    SUBSCRIBE_packet[3] = (char)(SUBSCRIBE_packet_identifier);
    SUBSCRIBE_packet[4] = (char)(topic_length >> 8);
    SUBSCRIBE_packet[5] = (char)(topic_length);
    int i;
    for (i = 6; i < 6 + strlen(topic); i++) {
        SUBSCRIBE_packet[i] = topic[i - 6];
    }
    SUBSCRIBE_packet[i] = QoS;
    
    // Sending
    usb_write(SUBSCRIBE_packet, SUBSCRIBE_packet_size);    
    delete SUBSCRIBE_packet;
}

bool SUBACK() {
    bool subscription_accepted = true;
    char SUBACK_ok[] = {0x90, 0x03, (char)(SUBSCRIBE_packet_identifier >> 8), (char)SUBSCRIBE_packet_identifier, 0x00};
    int noBytes = 5;
    char buffer[BUFSIZE];
    
    if (!usb.readable()) {
        return false;
    }
    else {
        usb_read(buffer, noBytes);
        for (int i = 0; i < noBytes; i++) {
            if (buffer[i] != SUBACK_ok[i]) {
                subscription_accepted = false;
            }
        }
    }
    
    return subscription_accepted;
}

float read_PUBLISH() {
    char buffer[BUFSIZE];
    char remaining_length = 0x00;
    unsigned short int topic_length = 0x0000;
    char topic[BUFSIZE];
    char value[BUFSIZE];
    char QoS;
    float toRet = -1.0;
    
    if (!usb.readable()) {
        toRet = -1.0;
    }
    else {
        usb_read(buffer, PUBLISHSIZE);
        // Is it a publish packet?
        if (buffer[0] == 0x30) {
            remaining_length = buffer[1];
            topic_length = (buffer[2] << 8) | buffer[3];
            int i;
            for (i = 4; i < 4 + topic_length; i++) {
                topic[i - 4] = buffer[i];
            }
            topic[topic_length] = '\0';
            int j;
            for (j = i; j < i + remaining_length - 3; j++) {
                value[j - i] = buffer[j];
            }
            value[j - i - 2] = '\0';
            QoS = j;
            
            toRet = atof(value);
        }
        else {
            toRet = -1.0;
        }
    }
    
    return toRet;    
}  

void setLeds(float red, float green, float blue) {
    red_led = red;
    green_led = green;
    blue_led = blue;   
}

int main() {    
    setLeds(1, 1, 1); 
    
    int connect_state_machine = 0;
    
    #ifdef SENDER
    int sender_state_machine = -1;
    float oldValue = 0.0;
    #endif
    
    #ifdef RECEIVER
    int receiver_state_machine = -1;
    float ledValue = 0.85;
    #endif
    while(1) {
        switch(connect_state_machine) {
            case 0:
                // Disconnected, trying to connect                                 
                CONNECT(600, "mbed");
                                    
                setLeds(0.85, 0.85, 1);
                
                connect_state_machine = 1;
                
                break;    
            case 1:
                bool connected = false;
                int i = 0;

                setLeds(0.85, 1, 1);

                while (!connected && i < 3) {               
                    connected = CONNACK();
                    i++;
                    wait_ms(SPEED);
                }

                setLeds(0.85, 0.85, 1);
               
                if (connected) {
                    connect_state_machine = -1;
                    #ifdef SENDER
                    sender_state_machine = 0;
                    #endif
                    #ifdef RECEIVER
                    receiver_state_machine = 0;
                    #endif
                }
                else {
                    wait_ms(SPEED);
                    connect_state_machine = 0;
                }
                break;        
        }
                            
        #ifdef SENDER       
        switch(sender_state_machine) {
            case 0:
                #ifndef RECEIVER
                setLeds(1, 0.85, 1);
                #endif

                char toSend[8];
                float value = touch.readPercentage();
                if (value != 0.0 && value != oldValue) {
                    oldValue = value;                
                    sprintf(toSend, "%.2f", value);
                    PUBLISH("LED", toSend);
                }
                wait_ms(SPEED);
                break;
        }
        #endif       
        
        #ifdef RECEIVER
        switch(receiver_state_machine) {
            case 0:
            {
                setLeds(1, 0.85, 1);
                SUBSCRIBE("LED");
                receiver_state_machine = 1;
                wait_ms(SPEED);
                break;
            }
            case 1:
            {
                bool subscribed = false;
                int i = 0;

                setLeds(0.85, 0.9, 1);

                while (!subscribed && i < 3) {               
                    subscribed = SUBACK();
                    i++;
                    wait_ms(SPEED);
                }

                setLeds(1, 0.85, 1);
               
                if (subscribed) {
                    receiver_state_machine = 2;
                }
                else {
                    wait_ms(SPEED);
                    receiver_state_machine = 0;
                }
                break;
            }
            case 2:
            {
                float readValue = -1.0;
                readValue = read_PUBLISH();
                if (readValue != -1.0) {
                    ledValue = readValue;
                }                
                setLeds(1, 1, ledValue);
                wait_ms(SPEED);            
                break;
            }
        }
        #endif               
    }
}
