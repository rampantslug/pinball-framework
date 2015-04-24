using Caliburn.Micro;
using MassTransit;
using RampantSlug.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.ServerLibrary.Events;

namespace RampantSlug.ServerLibrary.ContractImplementations
{
    class ConfigureMachineMessageConsumer : Consumes<IConfigureMachineMessage>.Context
    {
        public void Consume(IConsumeContext<IConfigureMachineMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new ConfigureMachineEvent()
            {
                UseHardware = message.Message.UseHardware
            });

        }

    }
}
