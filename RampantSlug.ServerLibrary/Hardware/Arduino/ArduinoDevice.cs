using System;
using System.Configuration;
using System.IO.Ports;
using RampantSlug.Common.Converters;
using RampantSlug.Common.Logging;
using RampantSlug.ServerLibrary.Logging;

namespace RampantSlug.ServerLibrary.Hardware.Arduino
{
    public class ArduinoDevice
    {
        public RsLogManager _logManager { get; set; }

        public string PortName { get; set; }

        /// <summary>
        /// Interface for the Serial Port at which an Arduino Board
        /// is connected.
        /// </summary>
        SerialPort arduinoBoard = new SerialPort();

        public ArduinoDevice()
        {
            PortName = "COM3";

            _logManager = RsLogManager.GetCurrent;
            _logManager.LogMessage(LogEventType.Info, OriginatorType.Arduino, "Uno", "Connecting", "Connecting to Arduino on " + PortName);

            try
            {
                OpenArduinoConnection();
            }
            catch (Exception)
            {
                _logManager.LogMessage(LogEventType.Error, OriginatorType.Arduino, "Uno", "Disconnected", "Failed to connect to Arduino on " + PortName);
            }

        }

        private void OpenArduinoConnection()
        {
            if (!arduinoBoard.IsOpen)
            {
                arduinoBoard.DataReceived += arduinoBoard_DataReceived;
                arduinoBoard.PortName = PortName;
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
                    _logManager.LogMessage(LogEventType.Error, OriginatorType.Arduino, "Uno", "Disconnected", "Failed to send data to Arduino on " + PortName);
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
