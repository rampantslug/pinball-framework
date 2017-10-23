using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using BusinessObjects.Devices;
using Logging;

namespace Common
{
    [Export(typeof(IDevices))]
    public class Devices : IDevices
    {
        // TODO: Probably pull these out into a file somewhere else...
        public const string ProcSwitchMatrix = "PSM";
        public const string ProcDirectSwitch = "PDS";
        public const string ProcDriverBoard = "PDB";
        public const string ProcLedBoard = "PLB";
        public const string ArduinoServoShield = "ASS";
        public const string ArduinoMotorShield = "AMS";


        public AttrCollection<ushort, string, Switch> Switches { get; set; }

        public Dictionary<ushort, ushort> SwitchIdAddressLookup { get; set; } 

        public AttrCollection<ushort, string, Coil> Coils { get; set; }

        public AttrCollection<ushort, string, StepperMotor> StepperMotors { get; set; }

        public AttrCollection<ushort, string, Servo> Servos { get; set; }

        public AttrCollection<ushort, string, Led> Leds { get; set; }


        
        public Devices()
        {
            // TODO: Need to pass in controller hardware at this point??

            SwitchIdAddressLookup = new Dictionary<ushort, ushort>();
        }

       


        private static int ParseAddressString(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return -1;
            }
            
            // Split out address into Controller and port
            var controllerPlusPort = address.Split('-');

            if (controllerPlusPort.Count() != 2)
            {
                // Invalid Address
                // Throw exception, error message and dont process the rest of this Device
                return -1;
            }

            switch (controllerPlusPort[0])
            {
                // Proc Switch Matrix Address
                case ProcSwitchMatrix:
                    {
                        return parse_matrix_num(controllerPlusPort[1]);
                    }

                // Proc Direct Switch Address
                case ProcDirectSwitch:
                    {
                        return Int32.Parse(controllerPlusPort[1]);
                    }

                // Proc Driver Board (Coil?) Address
                case ProcDriverBoard:
                    {
                        return Int32.Parse(controllerPlusPort[1]);
                    }

                // Proc Led Board Address
                case ProcLedBoard:
                    {
                        return Int32.Parse(controllerPlusPort[1]);
                    }

                // Arduino Servo Shield Address
                case ArduinoServoShield:
                    {
                        return Int32.Parse(controllerPlusPort[1]);
                    }

                // Arduino Motor Shield Address
                case ArduinoMotorShield:
                    {
                        return Int32.Parse(controllerPlusPort[1]);
                    }
                default:
                    {
                        return -1;
                    }
            }

        }

        private static int parse_matrix_num(string num)
        {
            string[] cr_list = num.Split('/');
            return (32 + Int32.Parse(cr_list[0]) * 16 + Int32.Parse(cr_list[1]));
        }

        #region Switches

        public void LoadSwitches(List<Switch> switches)
        {
            Switches = new AttrCollection<ushort, string, Switch>();

            // Reset the counter for switch ids.
            var deviceNumber = 1;

            foreach (var sw in switches)
            {
                if (sw != null)
                {
                    try
                    {
                        sw.Number = (ushort)deviceNumber;
                        Switches.Add(sw.Number, sw.Name, sw);

                        deviceNumber++;
                    }

                    catch
                        (Exception ex)
                    {
                        RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, sw.Name, "Configuration", "Problem with device config: " + ex.Message);
                    }
                }
            }
        }

        public bool AddSwitch(Switch sw)
        {
            // As it is a new device we need to generate a new number
            var deviceNumber = Switches.Values.Last().Number + 1;

            //var deviceNumber = ParseAddressString(sw.Address);
            
            if (deviceNumber == -1)
            {
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, sw.Name, "Configuration", "Invalid device address: " + sw.Address);
                return false;
            }
            else
            {
                Switches.Add((ushort)deviceNumber, sw.Name, sw);
                return true;
            }           
        }

        public void RemoveSwitch(ushort number)
        {
            Switches.Remove(number);
        }

        public void RemoveSwitch(Switch sw)
        {
            Switches.Remove(sw.Number);
        }

        public void UpdateSwitch(ushort number, Switch sw)
        {
            Switches.Remove(number);
            Switches.Add(number, sw.Name, sw);
        }

        public List<Switch> AllSwitches()
        {
            return Switches.Values;
        }

        #endregion


        #region Coils

        public void LoadCoils(List<Coil> coils)
        {
            Coils = new AttrCollection<ushort, string, Coil>();

            // Reset the counter for switch ids.
            var deviceNumber = 1;

            foreach (var coil in coils)
            {
                if (coil != null)
                {
                    try
                    {
                        coil.Number = (ushort)deviceNumber;
                        Coils.Add(coil.Number, coil.Name, coil);

                        deviceNumber++;
                    }
                    catch
                        (Exception ex)
                    {
                        RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, coil.Name, "Configuration", "Problem with device config: " + ex.Message);
                    }
                }
            }
        }

        public bool AddCoil(Coil coil)
        {
            // As it is a new device we need to generate a new number
            var deviceNumber = Coils.Values.Last().Number + 1;

            //var deviceNumber = ParseAddressString(coil.Address);
            if (deviceNumber == -1)
            {
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, coil.Name, "Configuration", "Invalid device address: " + coil.Address);
                return false;
            }
            else
            {
                Coils.Add((ushort) deviceNumber, coil.Name, coil);
                return true;
            }
        }

        public void RemoveCoil(ushort number)
        {
            Coils.Remove(number);
        }

        public void RemoveCoil(Coil coil)
        {
            Coils.Remove(coil.Number);
        }

        public void UpdateCoil(ushort number, Coil coil)
        {
            Coils.Remove(number);
            Coils.Add(number, coil.Name, coil);
        }

        public List<Coil> AllCoils()
        {
            return Coils.Values;
        }

        #endregion

        #region StepperMotors

        public void LoadStepperMotors(List<StepperMotor> stepperMotors)
        {
            StepperMotors = new AttrCollection<ushort, string, StepperMotor>();

            // Reset the counter for switch ids.
            var deviceNumber = 1;

            foreach (var stepperMotor in stepperMotors)
            {
                if (stepperMotor != null)
                {
                    try
                    {
                        stepperMotor.Number = (ushort)deviceNumber;
                        StepperMotors.Add(stepperMotor.Number, stepperMotor.Name, stepperMotor);

                        deviceNumber++;
                    }
                    catch
                        (Exception ex)
                    {
                        RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, stepperMotor.Name, "Configuration", "Problem with device config: " + ex.Message);
                    }
                }
            }
        }

        public bool AddStepperMotor(StepperMotor stepperMotor)
        {
            // As it is a new device we need to generate a new number
            var deviceNumber = Switches.Values.Last().Number + 1;
            //var deviceNumber = ParseAddressString(stepperMotor.Address);
            if (deviceNumber == -1)
            {
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, stepperMotor.Name, "Configuration", "Invalid device address: " + stepperMotor.Address);
                return false;
            }
            else
            {
                StepperMotors.Add((ushort) deviceNumber, stepperMotor.Name, stepperMotor);
                return true;
            }
        }

        public void RemoveStepperMotor(ushort number)
        {
            StepperMotors.Remove(number);
        }

        public void RemoveStepperMotor(StepperMotor stepperMotor)
        {
            StepperMotors.Remove(stepperMotor.Number);
        }

        public void UpdateStepperMotor(ushort number, StepperMotor stepperMotor)
        {
            StepperMotors.Remove(number);
            StepperMotors.Add(number, stepperMotor.Name, stepperMotor);
        }

        public List<StepperMotor> AllStepperMotors()
        {
            return StepperMotors.Values;
        }

        #endregion

        #region Servos

        public void LoadServos(List<Servo> servos)
        {
            Servos = new AttrCollection<ushort, string, Servo>();

            // Reset the counter for switch ids.
            var deviceNumber = 1;

            foreach (var servo in servos)
            {
                if (servo != null)
                {
                    try
                    {
                        servo.Number = (ushort)deviceNumber;
                        Servos.Add(servo.Number, servo.Name, servo);
                        
                        deviceNumber++;
                    }

                    catch
                        (Exception ex)
                    {
                        RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, servo.Name, "Configuration", "Problem with device config: " + ex.Message);
                    }
                }
            }
        }

        public bool AddServo(Servo servo)
        {
            // As it is a new device we need to generate a new number
            var deviceNumber = Switches.Values.Last().Number + 1;
            //var deviceNumber = ParseAddressString(servo.Address);
            if (deviceNumber == -1)
            {
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, servo.Name, "Configuration", "Invalid device address: " + servo.Address);
                return false;
            }
            else
            {
                Servos.Add((ushort)deviceNumber, servo.Name, servo);
                return true;
            }
        }

        public void RemoveServo(ushort number)
        {
            Servos.Remove(number);
        }

        public void RemoveServo(Servo servo)
        {
            Servos.Remove(servo.Number);
        }

        public void UpdateServo(ushort number, Servo servo)
        {
            Servos.Remove(number);
            Servos.Add(number, servo.Name, servo);
        }

        public List<Servo> AllServos()
        {
            return Servos.Values;
        }

        #endregion

        #region Leds

        public void LoadLeds(List<Led> leds)
        {
            Leds = new AttrCollection<ushort, string, Led>();

            // Reset the counter for led ids.
            var deviceNumber = 1;

            foreach (var led in leds)
            {
                if (led != null)
                {
                    try
                    {
                        led.Number = (ushort)deviceNumber;
                        Leds.Add(led.Number, led.Name, led);

                        deviceNumber++;
                    }

                    catch
                        (Exception ex)
                    {
                        RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, led.Name, "Configuration", "Problem with device config: " + ex.Message);
                    }
                }
            }
        }

        public bool AddLed(Led led)
        {
            // As it is a new device we need to generate a new number
            var deviceNumber = Switches.Values.Last().Number + 1;
            //var deviceNumber = ParseAddressString(led.Address);
            if (deviceNumber == -1)
            {
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, led.Name, "Configuration", "Invalid device address: " + led.Address);
                return false;
            }
            else
            {
                Leds.Add((ushort)deviceNumber, led.Name, led);
                return true;
            }
        }

        public void RemoveLed(ushort number)
        {
            Leds.Remove(number);
        }

        public void RemoveLed(Led led)
        {
            Leds.Remove(led.Number);
        }

        public void UpdateLed(ushort number, Led led)
        {
            Leds.Remove(number);
            Leds.Add(number, led.Name, led);
        }

        public List<Led> AllLeds()
        {
            return Leds.Values;
        }

        #endregion

    }
}
