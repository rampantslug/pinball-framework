using Caliburn.Micro;
using MassTransit;
using RampantSlug.Contracts;
using RampantSlug.PinballClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ContractImplementations
{
    class EventMessageConsumer: Consumes<IEventMessage>.Context
    {
        public void Consume(IConsumeContext<IEventMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DisplayMessageResults 
            {
                Timestamp = message.Message.Timestamp,
                EventType = "System",
                Name = "Event Message",
                State = "OK",                
                Information = message.Message.Message
                 
            });

        }

    }
}
