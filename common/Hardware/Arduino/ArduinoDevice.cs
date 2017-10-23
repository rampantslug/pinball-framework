using System;
using System.IO.Ports;
using Logging;

namespace Hardware.Arduino
{
    public class ArduinoDevice : IArduinoDevice
    {

        // TODO !!!
        /*

            Accept command as string?
            Verify string is of correct format
            OR provide methods with parameters and generate string internally (preferred?)

            Current Format

            #

        */



        public void RotateServo(string controllerName, uint servoId, double angle)
        {
            // 

            // Use Servo Shield
            if (controllerName.Equals("ASS"))
            {
                // Check valid id
                if (servoId < 16)
                {
                    if (angle >= 0 && angle <= 180)
                    {
                        
                    }

                }
            }

            // Use Motor Shield
            else if (controllerName.Equals("AMS"))
            {

            }

            else
            {
                
            }

            var controlString = "ASS" + "-" + servoId + "-" +  angle + "#";
            SendRequestToArduinoBoard(controlString);
        }

        public void RotateStepperMotor(string controllerName, int servoId, double angle)
        {
            //var controlString = "ASS" + "-" + servoId + "-" + angle + "#";
            //SendRequestToArduinoBoard(controlString);
        }









        public ArduinoDevice()
        {
            PortName = "COM3";

           // _logger = IoC.Get<IRsLogger>();
          //  _logger.LogMessage(LogEventType.Info, OriginatorType.Arduino, "Uno", "Connecting", "Connecting to Arduino on " + PortName);

            try
            {
                OpenArduinoConnection();
            }
            catch (Exception)
            {
           //     _logger.LogMessage(LogEventType.Error, OriginatorType.Arduino, "Uno", "Disconnected", "Failed to connect to Arduino on " + PortName);
            }

        }

        public void OpenArduinoConnection()
        {
            if (!_arduinoBoard.IsOpen)
            {
                _arduinoBoard.DataReceived += arduinoBoard_DataReceived;
                _arduinoBoard.PortName = PortName;
                _arduinoBoard.Open();
            }
            else
            {
                throw new InvalidOperationException("The Serial Port is already open!");
            }
        }

        /// <summary>
        /// Closes the connection to an Arduino Board.
        /// </summary>
        public void CloseArduinoConnection()
        {
            _arduinoBoard.Close();
        }


        private void SendRequestToArduinoBoard(string controllerString)
        {
            if (_arduinoBoard.IsOpen)
            {
                // Switch on the string that is sent through
                try
                {
                    _arduinoBoard.Write(controllerString);

                    /*if (string.Equals(tempControllerString, "Right"))
                    {
                        arduinoBoard.Write("ASS2#");
                    }

                    else if (string.Equals(tempControllerString, "Left"))
                    {
                        arduinoBoard.Write("ASS1#");
                    }*/
                }
                catch (System.IO.IOException)
                {
               //     _logger.LogMessage(LogEventType.Error, OriginatorType.Arduino, "Uno", "Disconnected", "Failed to send data to Arduino on " + PortName);
                }
            }
            else
            {
                throw new InvalidOperationException("Can't get data if the serial Port is closed!");
            }
        }


        void arduinoBoard_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
                    //Do nothing at the moment
        }


        public IRsLogger Logger { get; set; }

        public string PortName { get; set; }

        /// <summary>
        /// Interface for the Serial Port at which an Arduino Board
        /// is connected.
        /// </summary>
        readonly SerialPort _arduinoBoard = new SerialPort();

    }
}
