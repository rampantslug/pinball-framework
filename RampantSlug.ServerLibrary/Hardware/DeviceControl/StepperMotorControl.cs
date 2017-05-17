using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using RampantSlug.Common.Devices;
using RampantSlug.ServerLibrary.Events;

namespace RampantSlug.ServerLibrary.Hardware.DeviceControl
{
    public class StepperMotorControl : StepperMotor
    {
        public StepperMotor BaseStepperMotor { get; set; }

        public StepperMotorControl(StepperMotor stepperMotor)
        {
            BaseStepperMotor = stepperMotor;

           // Initialise all values from servo
            Number = stepperMotor.Number;
            Address = stepperMotor.Address;
            Name = stepperMotor.Name;
            State = stepperMotor.State;
            LastChangeTimeStamp = stepperMotor.LastChangeTimeStamp;
            //WiringColors = stepperMotor.WiringColors;
            VirtualLocationX = stepperMotor.VirtualLocationX;
            VirtualLocationY = stepperMotor.VirtualLocationY;
        }

        public void Rotate()
        {

        }

    }
}
