using Caliburn.Micro;
using MassTransit;
using RampantSlug.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ContractImplementations
{
    class ConfigMessageConsumer: Consumes<IConfigMessage>.Context
    {
        public void Consume(IConsumeContext<IConfigMessage> message)
        {    
            var shell = IoC.Get<IShell>();
            shell.UpdateViewModels(message.Message.MachineConfiguration);
        }

    }
}
