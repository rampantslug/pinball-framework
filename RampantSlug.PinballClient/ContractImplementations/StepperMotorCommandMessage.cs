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
    class StepperMotorCommandMessage: IStepperMotorCommandMessage
    {        
        public DateTime Timestamp { get; set; }

        public StepperMotor Device { get; set; }

        public string TempControllerMessage { get; set; }

    }
}
