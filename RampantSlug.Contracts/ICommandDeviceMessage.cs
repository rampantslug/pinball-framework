using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Contracts
{
    public interface ICommandSwitchMessage
    {        
        DateTime Timestamp { get; }

        Switch Device { get; }

        string TempControllerMessage { get; }
    }

    public interface ICommandCoilMessage
    {
        DateTime Timestamp { get; }

        Coil Device { get; }

        string TempControllerMessage { get; }
    }

    public interface ICommandStepperMotorMessage
    {
        DateTime Timestamp { get; }

        StepperMotor Device { get; }

        string TempControllerMessage { get; }
    }

    public interface ICommandServoMessage
    {
        DateTime Timestamp { get; }

        Servo Device { get; }

        string TempControllerMessage { get; }
    }
}
