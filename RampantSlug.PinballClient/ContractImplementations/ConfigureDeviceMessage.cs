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
    class ConfigureSwitchMessage: IConfigureSwitchMessage
    {        
        public DateTime Timestamp { get; set; }

        public Switch Device { get; set; }
    }

    class ConfigureCoilMessage : IConfigureCoilMessage
    {
        public DateTime Timestamp { get; set; }

        public Coil Device { get; set; }
    }

    class ConfigureStepperMotorMessage : IConfigureStepperMotorMessage
    {
        public DateTime Timestamp { get; set; }

        public StepperMotor Device { get; set; }
    }

    class ConfigureServoMessage : IConfigureServoMessage
    {
        public DateTime Timestamp { get; set; }

        public Servo Device { get; set; }
    }
}
