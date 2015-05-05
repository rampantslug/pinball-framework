using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;
using RampantSlug.Common.Devices;
using RampantSlug.ServerLibrary.Hardware.DeviceControl;
using RampantSlug.ServerLibrary.Logging;

namespace RampantSlug.ServerLibrary
{

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

        public AttrCollection<ushort, string, CoilControl> Coils { get; set; }

        public AttrCollection<ushort, string, StepperMotorControl> StepperMotors { get; set; }

        public AttrCollection<ushort, string, ServoControl> Servos { get; set; }

        public AttrCollection<ushort, string, LedControl> Leds { get; set; }


        
        public Devices()
        {
            // TODO: Need to pass in controller hardware at this point??
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
            foreach (var sw in switches)
            {
                if (sw != null)
                {
                    try
                    {
                        var deviceNumber = ParseAddressString(sw.Address);
                        if (deviceNumber == -1)
                        {
                            throw new Exception("Invalid device Address: " + sw.Address);
                        }
                        else
                        {
                            sw.Number = (ushort)deviceNumber;
                            Switches.Add(sw.Number, sw.Name, sw);
                        }

                    }

                    catch
                        (Exception ex)
                    {

                        RsLogManager.GetCurrent.LogTestMessage("Error with Device in config: " + sw.Name + " " + ex.Message);

                    }
                }
            }
        }

        public bool AddSwitch(Switch sw)
        {
            // As it is a new device we need to generate a new number
            var deviceNumber = ParseAddressString(sw.Address);
            if (deviceNumber == -1)
            {
                RsLogManager.GetCurrent.LogTestMessage("Invalid device address for: " + sw.Name + ". Address: "+ sw.Address);
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
            Coils = new AttrCollection<ushort, string, CoilControl>();
            foreach (var coil in coils)
            {
                if (coil != null)
                {
                    try
                    {
                        var address = ParseAddressString(coil.Address);
                        if (address == -1)
                        {
                            throw new Exception("Invalid device Address: " + coil.Address);
                        }
                        else
                        {
                            coil.Number = (ushort)address;
                            Coils.Add(coil.Number, coil.Name, new CoilControl(coil));
                        }

                    }

                    catch
                        (Exception ex)
                    {

                        RsLogManager.GetCurrent.LogTestMessage("Error with Device in config: " + coil.Name + " " + ex.Message);

                    }
                }
            }
        }

        public bool AddCoil(Coil coil)
        {
            // As it is a new device we need to generate a new number
            var deviceNumber = ParseAddressString(coil.Address);
            if (deviceNumber == -1)
            {
                RsLogManager.GetCurrent.LogTestMessage("Invalid device address for: " + coil.Name + ". Address: " + coil.Address);
                return false;
            }
            else
            {
                Coils.Add((ushort) deviceNumber, coil.Name, new CoilControl(coil));
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
            Coils.Add(number, coil.Name, new CoilControl(coil));
        }

        public List<Coil> AllCoils()
        {
            var coils = new List<Coil>();
            foreach (var coilControl in Coils)
            {
                coils.Add(coilControl.Value.BaseCoil);
            }
            return coils;
        }

        #endregion

        #region StepperMotors

        public void LoadStepperMotors(List<StepperMotor> stepperMotors)
        {
            StepperMotors = new AttrCollection<ushort, string, StepperMotorControl>();
            foreach (var stepperMotor in stepperMotors)
            {
                if (stepperMotor != null)
                {
                    try
                    {
                        var address = ParseAddressString(stepperMotor.Address);
                        if (address == -1)
                        {
                            throw new Exception("Invalid device Address: " + stepperMotor.Address);
                        }
                        else
                        {
                            stepperMotor.Number = (ushort)address;
                            StepperMotors.Add(stepperMotor.Number, stepperMotor.Name, new StepperMotorControl(stepperMotor));
                        }

                    }

                    catch
                        (Exception ex)
                    {

                        RsLogManager.GetCurrent.LogTestMessage("Error with Device in config: " + stepperMotor.Name + " " + ex.Message);

                    }
                }
            }
        }

        public bool AddStepperMotor(StepperMotor stepperMotor)
        {
            // As it is a new device we need to generate a new number
            var deviceNumber = ParseAddressString(stepperMotor.Address);
            if (deviceNumber == -1)
            {
                RsLogManager.GetCurrent.LogTestMessage("Invalid device address for: " + stepperMotor.Name + ". Address: " + stepperMotor.Address);
                return false;
            }
            else
            {
                StepperMotors.Add((ushort) deviceNumber, stepperMotor.Name, new StepperMotorControl(stepperMotor));
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
            StepperMotors.Add(number, stepperMotor.Name, new StepperMotorControl(stepperMotor));
        }

        public List<StepperMotor> AllStepperMotors()
        {
            var stepperMotors = new List<StepperMotor>();
            foreach (var servoControl in StepperMotors)
            {
                stepperMotors.Add(servoControl.Value.BaseStepperMotor);
            }
            return stepperMotors;

        }

        #endregion

        #region Servos

        public void LoadServos(List<Servo> servos)
        {
            Servos = new AttrCollection<ushort, string, ServoControl>();
            foreach (var servo in servos)
            {
                if (servo != null)
                {
                    try
                    {
                        var address = ParseAddressString(servo.Address);
                        if (address == -1)
                        {
                            throw new Exception("Invalid device Address: " + servo.Address);
                        }
                        else
                        {
                            servo.Number = (ushort)address;
                            Servos.Add(servo.Number, servo.Name, new ServoControl(servo));
                        }

                    }

                    catch
                        (Exception ex)
                    {

                        RsLogManager.GetCurrent.LogTestMessage("Error with Device in config: " + servo.Name + " " + ex.Message);

                    }
                }
            }
        }

        public bool AddServo(Servo servo)
        {
            // As it is a new device we need to generate a new number
            var deviceNumber = ParseAddressString(servo.Address);
            if (deviceNumber == -1)
            {
                RsLogManager.GetCurrent.LogTestMessage("Invalid device address for: " + servo.Name + ". Address: " + servo.Address);
                return false;
            }
            else
            {
                Servos.Add((ushort)deviceNumber, servo.Name, new ServoControl(servo));
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
            Servos.Add(number, servo.Name, new ServoControl(servo));
        }

        public List<Servo> AllServos()
        {


            var servos = new List<Servo>();
            foreach (var servoControl in Servos)
            {
                servos.Add(servoControl.Value.BaseServo);
            }
            return servos;

        }

        #endregion

        #region Leds

        public void LoadLeds(List<Led> leds)
        {
            Leds = new AttrCollection<ushort, string, LedControl>();
            foreach (var led in leds)
            {
                if (led != null)
                {
                    try
                    {
                        var address = ParseAddressString(led.Address);
                        if (address == -1)
                        {
                            throw new Exception("Invalid device Address: " + led.Address);
                        }
                        else
                        {
                            led.Number = (ushort)address;
                            Leds.Add(led.Number, led.Name, new LedControl(led));
                        }

                    }

                    catch
                        (Exception ex)
                    {

                        RsLogManager.GetCurrent.LogTestMessage("Error with Device in config: " + led.Name + " " + ex.Message);

                    }
                }
            }
        }

        public bool AddLed(Led led)
        {
            // As it is a new device we need to generate a new number
            var deviceNumber = ParseAddressString(led.Address);
            if (deviceNumber == -1)
            {
                RsLogManager.GetCurrent.LogTestMessage("Invalid device address for: " + led.Name + ". Address: " + led.Address);
                return false;
            }
            else
            {
                Leds.Add((ushort)deviceNumber, led.Name, new LedControl(led));
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
            Leds.Add(number, led.Name, new LedControl(led));
        }

        public List<Led> AllLeds()
        {
            var leds = new List<Led>();
            foreach (var ledControl in Leds)
                {
                    leds.Add(ledControl.Value.BaseLed);
                }
                return leds;

        }

        #endregion

    }
}
