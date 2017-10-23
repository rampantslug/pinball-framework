using Caliburn.Micro;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageContracts;
using ServerLibrary.Events;

namespace ServerLibrary.ContractImplementations
{
    class ConfigureMachineMessageConsumer : IConsumer<IConfigureMachineMessage>
    {
        public async Task Consume(ConsumeContext<IConfigureMachineMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            await eventAggregator.PublishOnUIThreadAsync(new ConfigureMachineEvent()
            {
                UseHardware = message.Message.UseHardware
            });

        }

    }
}
