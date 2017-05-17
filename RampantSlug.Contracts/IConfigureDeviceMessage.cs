using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Contracts
{
    /// <summary>
    /// Message containing configuration data of a single switch.
    /// </summary>
    public interface IConfigureSwitchMessage
    {       
        DateTime Timestamp { get; }

        Switch Device { get; }

        bool RemoveDevice { get; }
    }

    /// <summary>
    /// Message containing configuration data of a single coil.
    /// </summary>
    public interface IConfigureCoilMessage
    {
        DateTime Timestamp { get; }

        Coil Device { get; }

        bool RemoveDevice { get; }
    }

    /// <summary>
    /// Message containing configuration data of a single stepper motor.
    /// </summary>
    public interface IConfigureStepperMotorMessage
    {
        DateTime Timestamp { get; }

        StepperMotor Device { get; }

        bool RemoveDevice { get; }
    }

    /// <summary>
    /// Message containing configuration data of a single servo.
    /// </summary>
    public interface IConfigureServoMessage
    {
        DateTime Timestamp { get; }

        Servo Device { get; }

        bool RemoveDevice { get; }
    }

    /// <summary>
    /// Message containing configuration data of a single led.
    /// </summary>
    public interface IConfigureLedMessage
    {
        DateTime Timestamp { get; }

        Led Device { get; }

        bool RemoveDevice { get; }
    }
}
