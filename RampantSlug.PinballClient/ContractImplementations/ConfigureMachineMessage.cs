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
    class ConfigureMachineMessage: IConfigureMachineMessage
    {        
        public DateTime Timestamp { get; set; }

        public bool UseHardware { get; set; }
    }

}
