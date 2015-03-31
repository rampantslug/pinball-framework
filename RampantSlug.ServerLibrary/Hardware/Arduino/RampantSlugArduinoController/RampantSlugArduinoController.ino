
/*
 Stepper Motor Control - one step at a time

 This program drives a unipolar or bipolar stepper motor.
 The motor is attached to digital pins 8 - 11 of the Arduino.

 The motor will step one step at a time, very slowly.  You can use this to
 test that you've got the four wires of your stepper wired to the correct
 pins. If wired correctly, all steps should be in the same direction.

 Use this also to count the number of steps per revolution of your motor,
 if you don't know it.  Then plug that number into the oneRevolution
 example to see if you got it right.

 Created 30 Nov. 2009
 by Tom Igoe

 */
// INCLUDES
#include <Stepper.h>

/*   CONSTANTS                                                    */
#define LED_TURN_ON_TIMEOUT  1000       //Timeout for LED power time (defines how long the LED stays powered on) in milliseconds
#define LED_PIN 13                      //Pin number on which the LED is connected
#define SERIAL_BAUDRATE 9600            //Baud-Rate of the serial Port
#define STX "\x02"                      //ASCII-Code 02, text representation of the STX code
#define ETX "\x03"                      //ASCII-Code 03, text representation of the ETX code
#define RS  "$"                         //Used as RS code

const int stepsPerRevolution = 200;  // change this to fit the number of steps per revolution
// for your motor

// initialize the stepper library on pins 8 through 11:
Stepper myStepper(stepsPerRevolution, 8, 9, 10, 11);

int stepCount = 0;         // number of steps the motor has taken





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
}


void setup() {
  // initialize the serial port:
  Serial.begin(9600);
}

void loop() {
	String command = "";  //Used to store the latest received command
  int serialResult = 0; //return value for reading operation method on serial in put buffer
  
  serialResult = readSerialInputCommand(&command);
  if(serialResult == MSG_METHOD_SUCCESS){
    if(command == "1#"){//Request for sending weather data via Serial Interface
                        //For demonstration purposes this only writes dummy data
        RotateStepperMotorRight();
    }
	else if(command == "1#")
	{
	RotateStepperMotorLeft();
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

void RotateStepperMotorRight(){
  // step one step:
  myStepper.step(10);
  Serial.print("steps:" );
  //Serial.println(stepCount);
  //stepCount++;
 // delay(500);
}

void RotateStepperMotorLeft(){
  // step one step:
  myStepper.step(-10);
  Serial.print("steps:" );
  //Serial.println(stepCount);
  //stepCount++;
 // delay(500);
}


  
}

