using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Contracts
{
    public interface IConfigureSwitchMessage
    {       
        DateTime Timestamp { get; }

        Switch Device { get; } 
    }

    public interface IConfigureCoilMessage
    {
        DateTime Timestamp { get; }

        Coil Device { get; }
    }

    public interface IConfigureStepperMotorMessage
    {
        DateTime Timestamp { get; }

        StepperMotor Device { get; }
    }

    public interface IConfigureServoMessage
    {
        DateTime Timestamp { get; }

        Servo Device { get; }
    }
}
