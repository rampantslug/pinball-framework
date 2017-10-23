using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;

namespace ServerLibrary.Events
{
    public class ConfigureSwitchEvent
    {
        public Switch Device { get; set; }

        public bool RemoveDevice { get; set; }
    }

    public class ConfigureCoilEvent
    {
        public Coil Device { get; set; }

        public bool RemoveDevice { get; set; }
    }

    public class ConfigureStepperMotorEvent
    {
        public StepperMotor Device { get; set; }

        public bool RemoveDevice { get; set; }
    }

    public class ConfigureServoEvent
    {
        public Servo Device { get; set; }

        public bool RemoveDevice { get; set; }
    }

    public class ConfigureLedEvent
    {
        public Led Device { get; set; }

        public bool RemoveDevice { get; set; }
    }
}
