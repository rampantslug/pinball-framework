using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common.Commands;

namespace RampantSlug.Contracts
{
    /// <summary>
    /// Message to initiate a remote switch command.
    /// </summary>
    public interface ICommandSwitchMessage
    {        
        DateTime Timestamp { get; }

        Switch Device { get; }

        SwitchCommand Command { get; }
    }

    /// <summary>
    /// Message to initiate a remote coil command.
    /// </summary>
    public interface ICommandCoilMessage
    {
        DateTime Timestamp { get; }

        Coil Device { get; }

        CoilCommand Command { get; }
    }

    /// <summary>
    /// Message to initiate a remote stepper motor command.
    /// </summary>
    public interface ICommandStepperMotorMessage
    {
        DateTime Timestamp { get; }

        StepperMotor Device { get; }

        StepperMotorCommand Command { get; }
    }

    /// <summary>
    /// Message to initiate a remote servo command.
    /// </summary>
    public interface ICommandServoMessage
    {
        DateTime Timestamp { get; }

        Servo Device { get; }

        ServoCommand Command { get; }
    }

    /// <summary>
    /// Message to initiate a remote led command.
    /// </summary>
    public interface ICommandLedMessage
    {
        DateTime Timestamp { get; }

        Led Device { get; }

        LedCommand Command { get; }
    }
}
