using System;
using System.Configuration;
using System.IO.Ports;
using RampantSlug.ServerLibrary.Logging;

namespace RampantSlug.ServerLibrary.Hardware.Arduino
{
    public class ArduinoDevice
    {
        public RsLogManager _logManager { get; set; }

        /// <summary>
        /// Interface for the Serial Port at which an Arduino Board
        /// is connected.
        /// </summary>
        SerialPort arduinoBoard = new SerialPort();

        public ArduinoDevice()
        {
            _logManager = RsLogManager.GetCurrent;
            _logManager.LogTestMessage("Initializing Arduino-Uno device...");

            try
            {
                OpenArduinoConnection();
            }
            catch (Exception)
            {
                _logManager.LogTestMessage("Error: Can not connect to the Arduino Board");

                //MessageBox.Show("Error: Can not connect to the Arduino Board - Configure the COM Port in the app.config file and check whether an Arduino Board is connected to your computer.");
            }

        }

        private void OpenArduinoConnection()
        {
            if (!arduinoBoard.IsOpen)
            {
                arduinoBoard.DataReceived += arduinoBoard_DataReceived;
                arduinoBoard.PortName = "COM3";
                arduinoBoard.Open();
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
            arduinoBoard.Close();
        }

        /// <summary>
        /// Sends the command to the Arduino board which triggers the board
        /// to send the weather data it has internally stored.
        /// </summary>
        public void SendRequestToArduinoBoard(string tempControllerString)
        {
            if (arduinoBoard.IsOpen)
            {
                // Switch on the string that is sent through
                try
                {
                    if (string.Equals(tempControllerString, "Right"))
                    {
                        arduinoBoard.Write("1#");
                    }

                    else if (string.Equals(tempControllerString, "Left"))
                    {
                        arduinoBoard.Write("2#");
                    }
                }
                catch (System.IO.IOException ex)
                {
                    _logManager.LogTestMessage("Error: Arduino is not connected");
                }
            }
            else
            {
                throw new InvalidOperationException("Can't get weather data if the serial Port is closed!");
            }
        }

        /// <summary>
        /// Reads weather data from the arduinoBoard serial port
        /// </summary>
        void arduinoBoard_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
                    //Do nothing at the moment
        }

    }
}
