﻿using System;
using BusinessObjects.Devices;

namespace MessageContracts
{
    /// <summary>
    /// Message containing updated state information of a switch.
    /// </summary>
    public interface IUpdateSwitchMessage
    {       
        DateTime Timestamp { get; }

        Switch Device { get; } 
    }

    /// <summary>
    /// Message containing updated state information of a coil.
    /// </summary>
    public interface IUpdateCoilMessage
    {
        DateTime Timestamp { get; }

        Coil Device { get; }
    }

    /// <summary>
    /// Message containing updated state information of a stepper motor.
    /// </summary>
    public interface IUpdateStepperMotorMessage
    {
        DateTime Timestamp { get; }

        StepperMotor Device { get; }
    }

    /// <summary>
    /// Message containing updated state information of a servo.
    /// </summary>
    public interface IUpdateServoMessage
    {
        DateTime Timestamp { get; }

        Servo Device { get; }
    }

    /// <summary>
    /// Message containing updated state information of a led.
    /// </summary>
    public interface IUpdateLedMessage
    {
        DateTime Timestamp { get; }

        Led Device { get; }
    }
}
