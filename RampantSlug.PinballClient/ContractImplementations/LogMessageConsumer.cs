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
    class LogMessageConsumer: Consumes<ILogMessage>.Context
    {
        public void Consume(IConsumeContext<ILogMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new LogEvent 
            {
                Timestamp = message.Message.Timestamp,
                EventType = message.Message.EventType,
                OriginatorType = message.Message.OriginatorType,
                OriginatorName = message.Message.OriginatorName,
                Status = message.Message.Status,                
                Information = message.Message.Information                 
            });

        }

    }
}
