using Caliburn.Micro;
using MassTransit;
using RampantSlug.Common.Devices;
using RampantSlug.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common.Commands;

namespace RampantSlug.PinballClient.ContractImplementations
{
    class CommandSwitchMessage: ICommandSwitchMessage
    {        
        public DateTime Timestamp { get; set; }

        public Switch Device { get; set; }

        public SwitchCommand Command { get; set; }

    }

    class CommandCoilMessage : ICommandCoilMessage
    {
        public DateTime Timestamp { get; set; }

        public Coil Device { get; set; }

        public CoilCommand Command { get; set; }

    }

    class CommandStepperMotorMessage : ICommandStepperMotorMessage
    {
        public DateTime Timestamp { get; set; }

        public StepperMotor Device { get; set; }

        public StepperMotorCommand Command { get; set; }

    }

    class CommandServoMessage : ICommandServoMessage
    {
        public DateTime Timestamp { get; set; }

        public Servo Device { get; set; }

        public ServoCommand Command { get; set; }

    }

    class CommandLedMessage : ICommandLedMessage
    {
        public DateTime Timestamp { get; set; }

        public Led Device { get; set; }

        public LedCommand Command { get; set; }

    }
}
