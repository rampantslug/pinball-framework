using System;
using RampantSlug.Common.Commands;
using RampantSlug.Common.Devices;

namespace RampantSlug.ServerLibrary.Events
{
    public class SwitchCommandResult
    {
        public DateTime Timestamp { get; set; }

        public Switch Device { get; set; }

        public SwitchCommand Command { get; set; }
    }

    public class CoilCommandResult
    {
        public DateTime Timestamp { get; set; }

        public Coil Device { get; set; }

        public CoilCommand Command { get; set; }
    }

    public class StepperMotorCommandResult
    {
        public DateTime Timestamp { get; set; }

        public StepperMotor Device { get; set; }

        public StepperMotorCommand Command { get; set; }
    }

    public class ServoCommandResult
    {
        public DateTime Timestamp { get; set; }

        public Servo Device { get; set; }

        public ServoCommand Command { get; set; }
    }

    public class LedCommandResult
    {
        public DateTime Timestamp { get; set; }

        public Led Device { get; set; }

        public LedCommand Command { get; set; }
    }
}
