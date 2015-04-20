using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common.Commands;

namespace RampantSlug.Contracts
{
    public interface ICommandSwitchMessage
    {        
        DateTime Timestamp { get; }

        Switch Device { get; }

        SwitchCommand Command { get; }
    }

    public interface ICommandCoilMessage
    {
        DateTime Timestamp { get; }

        Coil Device { get; }

        CoilCommand Command { get; }
    }

    public interface ICommandStepperMotorMessage
    {
        DateTime Timestamp { get; }

        StepperMotor Device { get; }

        StepperMotorCommand Command { get; }
    }

    public interface ICommandServoMessage
    {
        DateTime Timestamp { get; }

        Servo Device { get; }

        ServoCommand Command { get; }
    }

    public interface ICommandLedMessage
    {
        DateTime Timestamp { get; }

        Led Device { get; }

        LedCommand Command { get; }
    }
}
