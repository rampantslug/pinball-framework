using Caliburn.Micro;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using MessageContracts;
using Common.Commands;

namespace PinballClient.ContractImplementations
{
    /// <summary>
    /// Message to initiate a remote switch command.
    /// </summary>
    class CommandSwitchMessage: ICommandSwitchMessage
    {        
        public DateTime Timestamp { get; set; }

        public Switch Device { get; set; }

        public SwitchCommand Command { get; set; }

    }

    /// <summary>
    /// Message to initiate a remote coil command.
    /// </summary>
    class CommandCoilMessage : ICommandCoilMessage
    {
        public DateTime Timestamp { get; set; }

        public Coil Device { get; set; }

        public CoilCommand Command { get; set; }

    }

    /// <summary>
    /// Message to initiate a remote stepper motor command.
    /// </summary>
    class CommandStepperMotorMessage : ICommandStepperMotorMessage
    {
        public DateTime Timestamp { get; set; }

        public StepperMotor Device { get; set; }

        public StepperMotorCommand Command { get; set; }

    }

    /// <summary>
    /// Message to initiate a remote servo command.
    /// </summary>
    class CommandServoMessage : ICommandServoMessage
    {
        public DateTime Timestamp { get; set; }

        public Servo Device { get; set; }

        public ServoCommand Command { get; set; }

    }

    /// <summary>
    /// Message to initiate a remote led command.
    /// </summary>
    class CommandLedMessage : ICommandLedMessage
    {
        public DateTime Timestamp { get; set; }

        public Led Device { get; set; }

        public LedCommand Command { get; set; }

    }
}
