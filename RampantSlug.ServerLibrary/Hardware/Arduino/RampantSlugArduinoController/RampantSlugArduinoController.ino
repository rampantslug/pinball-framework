

// INCLUDES
#include <Wire.h>
#include <Adafruit_MotorShield.h>
#include "utility/Adafruit_PWMServoDriver.h"

/*   CONSTANTS                                                    */
#define LED_TURN_ON_TIMEOUT  1000       //Timeout for LED power time (defines how long the LED stays powered on) in milliseconds
#define LED_PIN 13                      //Pin number on which the LED is connected
#define SERIAL_BAUDRATE 9600            //Baud-Rate of the serial Port
#define STX "\x02"                      //ASCII-Code 02, text representation of the STX code
#define ETX "\x03"                      //ASCII-Code 03, text representation of the ETX code
#define RS  "$"                         //Used as RS code

 /*   WARNING, ERROR AND STATUS CODES                              */
//STATUS
#define MSG_METHOD_SUCCESS 0                      //Code which is used when an operation terminated  successfully
//WARNINGS
#define WRG_NO_SERIAL_DATA_AVAILABLE 250            //Code indicates that no new data is available at the serial input buffer
//ERRORS
#define ERR_SERIAL_IN_COMMAND_NOT_TERMINATED -1   //Code is used when a serial input commands' last char is not a '#' 

const int stepsPerRevolution = 200;  // change this to fit the number of steps per revolution
// Create the motor shield object with the default I2C address
Adafruit_MotorShield AFMS = Adafruit_MotorShield(); 
// Or, create it with a different I2C address (say for stacking)
// Adafruit_MotorShield AFMS = Adafruit_MotorShield(0x61); 

// Connect a stepper motor with 200 steps per revolution (1.8 degree)
// to motor port #2 (M3 and M4)
Adafruit_StepperMotor *myMotor = AFMS.getStepper(200, 2);





/*   METHODS    
******************************************************************
The setup method is executed once after the bootloader is done
with his job.
******************************************************************/
void setup(){
  //setup the LED pin for output
  pinMode(LED_PIN, OUTPUT);
  //setup serial pin
  Serial.begin(SERIAL_BAUDRATE);

    Serial.println("Stepper test!");

  AFMS.begin();  // create with the default frequency 1.6KHz
  //AFMS.begin(1000);  // OR with a different frequency, say 1KHz
  
  myMotor->setSpeed(10);  // 10 rpm 
}


void loop() {
	String command = "";  //Used to store the latest received command
  int serialResult = 0; //return value for reading operation method on serial in put buffer
  
  serialResult = readSerialInputCommand(&command);
  if(serialResult == MSG_METHOD_SUCCESS){
    if(command == "1#"){//Request for sending weather data via Serial Interface
                        //For demonstration purposes this only writes dummy data
         myMotor->step(100, FORWARD, SINGLE); 
  
    }
	else if(command == "2#")
	{
	myMotor->step(100, BACKWARD, SINGLE); 
	}
  }
  
  if(serialResult == WRG_NO_SERIAL_DATA_AVAILABLE){//If there is no data AVAILABLE at the serial port, let the LED blink
     digitalWrite(LED_PIN, HIGH);
     delay(250);
     digitalWrite(LED_PIN, LOW);
     delay(250);
  }
  else{
    if(serialResult == ERR_SERIAL_IN_COMMAND_NOT_TERMINATED){//If the command format was invalid, the led is turned off for two seconds
      digitalWrite(LED_PIN, LOW);
      delay(2000);
    }
  }
}

  int readSerialInputCommand(String *command){
  
  int operationStatus = MSG_METHOD_SUCCESS;//Default return is MSG_METHOD_SUCCESS reading data from com buffer.
  
  //check if serial data is available for reading
  if (Serial.available()) {
     char serialInByte;//temporary variable to hold the last serial input buffer character
     
     do{//Read serial input buffer data byte by byte 
       serialInByte = Serial.read();
       *command = *command + serialInByte;//Add last read serial input buffer byte to *command pointer
     }while(serialInByte != '#' && Serial.available());//until '#' comes up or no serial data is available anymore
     
     if(serialInByte != '#') {
       operationStatus = ERR_SERIAL_IN_COMMAND_NOT_TERMINATED;
     }
  }
  else{//If not serial input buffer data is AVAILABLE, operationStatus becomes WRG_NO_SERIAL_DATA_AVAILABLE (= No data in the serial input buffer AVAILABLE)
    operationStatus = WRG_NO_SERIAL_DATA_AVAILABLE;
  }
  
  return operationStatus;
}

 

