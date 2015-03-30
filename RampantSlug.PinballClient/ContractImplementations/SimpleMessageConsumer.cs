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
    class SimpleMessageConsumer: Consumes<ISimpleMessage>.Context
    {
        public void Consume(IConsumeContext<ISimpleMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DisplayMessageResults 
            {
                Timestamp = message.Message.Timestamp,
                EventType = "System",
                Name = "Simple Message",
                State = "OK",
                Information = message.Message.Message
            });

        }

    }
}
