using Caliburn.Micro;
using MassTransit;
using RampantSlug.Common.Devices;
using RampantSlug.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ContractImplementations
{
    class CommandSwitchMessage: ICommandSwitchMessage
    {        
        public DateTime Timestamp { get; set; }

        public Switch Device { get; set; }

        public string TempControllerMessage { get; set; }

    }

    class CommandCoilMessage : ICommandCoilMessage
    {
        public DateTime Timestamp { get; set; }

        public Coil Device { get; set; }

        public string TempControllerMessage { get; set; }

    }

    class CommandStepperMotorMessage : ICommandStepperMotorMessage
    {
        public DateTime Timestamp { get; set; }

        public StepperMotor Device { get; set; }

        public string TempControllerMessage { get; set; }

    }

    class CommandServoMessage : ICommandServoMessage
    {
        public DateTime Timestamp { get; set; }

        public Servo Device { get; set; }

        public string TempControllerMessage { get; set; }

    }
}
