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
    class RequestConfigMessageConsumer : IConsumer<IRequestConfigMessage>
    {
        public async Task Consume(ConsumeContext<IRequestConfigMessage> message)
        {
            //TODO: Do we need access to the configuration or general 'game' info at this point rather than marshalling to the event Aggregator
            
            var eventAggregator = IoC.Get<IEventAggregator>();

            await eventAggregator.PublishOnUIThreadAsync(new RequestConfigEvent());

        }

    }
}
