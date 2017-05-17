using System;
using System.Windows.Media;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.Events
{
    public class UpdateSwitch
    {
        public DateTime Timestamp { get; set; }

        public Switch Device { get; set; } 
    }

    public class UpdateCoil
    {
        public DateTime Timestamp { get; set; }

        public Coil Device { get; set; }
    }

    public class UpdateStepperMotor
    {
        public DateTime Timestamp { get; set; }

        public StepperMotor Device { get; set; }
    }

    public class UpdateServo
    {
        public DateTime Timestamp { get; set; }

        public Servo Device { get; set; }
    }

    public class UpdateLed
    {
        public DateTime Timestamp { get; set; }

        public Led Device { get; set; }
    }
}
