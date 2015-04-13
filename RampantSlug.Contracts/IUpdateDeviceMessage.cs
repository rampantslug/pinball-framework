using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Contracts
{
    public interface IUpdateSwitchMessage
    {       
        DateTime Timestamp { get; }

        Switch Device { get; } 
    }

    public interface IUpdateCoilMessage
    {
        DateTime Timestamp { get; }

        Coil Device { get; }
    }

    public interface IUpdateStepperMotorMessage
    {
        DateTime Timestamp { get; }

        StepperMotor Device { get; }
    }

    public interface IUpdateServoMessage
    {
        DateTime Timestamp { get; }

        Servo Device { get; }
    }
}
