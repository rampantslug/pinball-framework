using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using Logging;

namespace Common
{
    public class MachineState
    {
        public IDevices Devices { get; private set; }

        [ImportingConstructor]
        public MachineState(IDevices devices)
        {
            Devices = devices;
        }


        public bool UpdateDevice(Switch updatedSwitch, bool removeDevice)
        {
            // Adding a new switch
            if (updatedSwitch.Number == 0)
            {
                if (Devices.AddSwitch(updatedSwitch))
                {
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedSwitch.Name, "Add to config", "Added switch to config.");
                    return true;
                }
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, updatedSwitch.Name, "Add to config", "Invalid switch settings. Not saving to config.");
                return false;
            }

            // Update or remove existing Switch
            if (Devices.Switches.ContainsKey(updatedSwitch.Number))
            {
                if (removeDevice)
                {
                    Devices.RemoveSwitch(updatedSwitch.Number);
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedSwitch.Name, "Remove from config", "Removed switch from config.");
                }
                else
                {
                    Devices.UpdateSwitch(updatedSwitch.Number, updatedSwitch);
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedSwitch.Name, "Update config", "Updated switch in config.");
                }
                return true;
            }
            // Something has gone wrong. Should have generated a Number based on Address
            RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, updatedSwitch.Name, "Configuration", "Invalid switch settings. Not saving to config.");
            return false;
        }

        public bool UpdateDevice(Coil updatedCoil, bool removeDevice)
        {
            // Adding a new coil
            if (updatedCoil.Number == 0)
            {
                if (Devices.AddCoil(updatedCoil))
                {
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedCoil.Name, "Add to config", "Added coil to config.");
                    return true;
                }
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, updatedCoil.Name, "Add to config", "Invalid coil settings. Not saving to config.");
                return false;
            }

            // Update or remove existing coil
            if (Devices.Coils.ContainsKey(updatedCoil.Number))
            {
                if (removeDevice)
                {
                    Devices.RemoveCoil(updatedCoil.Number);
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedCoil.Name, "Remove from config", "Removed coil from config.");
                }
                else
                {
                    Devices.UpdateCoil(updatedCoil.Number, updatedCoil);
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedCoil.Name, "Update config", "Updated coil in config.");
                }
                return true;
            }
            else
            {
                // Something has gone wrong. Should have generated a Number based on Address
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, updatedCoil.Name, "Configuration", "Invalid coil settings. Not saving to config.");
                return false;
            }
        }

        public bool UpdateDevice(StepperMotor updatedStepperMotor, bool removeDevice)
        {
            // Adding a new stepperMotor
            if (updatedStepperMotor.Number == 0)
            {
                if (Devices.AddStepperMotor(updatedStepperMotor))
                {
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedStepperMotor.Name, "Add to config", "Added stepper motor to config.");
                    return true;
                }
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, updatedStepperMotor.Name, "Add to config", "Invalid stepper motor settings. Not saving to config.");
                return false;
            }

            // Update or remove existing stepperMotor
            if (Devices.StepperMotors.ContainsKey(updatedStepperMotor.Number))
            {
                if (removeDevice)
                {
                    Devices.RemoveStepperMotor(updatedStepperMotor.Number);
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedStepperMotor.Name, "Remove from config", "Removed stepper motor from config.");
                }
                else
                {
                    Devices.UpdateStepperMotor(updatedStepperMotor.Number, updatedStepperMotor);
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedStepperMotor.Name, "Update config", "Updated stepper motor in config.");
                }
                return true;
            }
            else
            {
                // Something has gone wrong. Should have generated a Number based on Address
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, updatedStepperMotor.Name, "Configuration", "Invalid stepper motor settings. Not saving to config.");
                return false;
            }
        }

        public bool UpdateDevice(Servo updatedServo, bool removeDevice)
        {
            // Adding a new servo
            if (updatedServo.Number == 0)
            {
                if (Devices.AddServo(updatedServo))
                {
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedServo.Name, "Add to config", "Added servo to config.");
                    return true;
                }
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, updatedServo.Name, "Add to config", "Invalid servo settings. Not saving to config.");
                return false;
            }

            // Update or remove existing servo
            if (Devices.Servos.ContainsKey(updatedServo.Number))
            {
                if (removeDevice)
                {
                    Devices.RemoveServo(updatedServo.Number);
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedServo.Name, "Remove from config", "Removed servo from config.");
                }
                else
                {
                    Devices.UpdateServo(updatedServo.Number, updatedServo);
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedServo.Name, "Update config", "Updated servo in config.");
                }
                return true;
            }
            else
            {
                // Something has gone wrong. Should have generated a Number based on Address
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, updatedServo.Name, "Configuration", "Invalid servo settings. Not saving to config.");
                return false;
            }
        }

        public bool UpdateDevice(Led updatedLed, bool removeDevice)
        {
            // Adding a new led
            if (updatedLed.Number == 0)
            {
                if (Devices.AddLed(updatedLed))
                {
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedLed.Name, "Add to config", "Added led to config.");
                    return true;
                }
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, updatedLed.Name, "Add to config", "Invalid led settings. Not saving to config.");
                return false;
            }

            // Update or remove existing led
            if (Devices.Leds.ContainsKey(updatedLed.Number))
            {
                if (removeDevice)
                {
                    Devices.RemoveLed(updatedLed.Number);
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedLed.Name, "Remove from config", "Removed led from config.");
                }
                else
                {
                    Devices.UpdateLed(updatedLed.Number, updatedLed);
                    RsLog.LogMessage(LogEventType.Info, OriginatorType.System, updatedLed.Name, "Update config", "Updated led in config.");
                }
                return true;
            }
            else
            {
                // Something has gone wrong. Should have generated a Number based on Address
                RsLog.LogMessage(LogEventType.Warn, OriginatorType.System, updatedLed.Name, "Configuration", "Invalid led settings. Not saving to config.");
                return false;
            }
        }
    }
}
