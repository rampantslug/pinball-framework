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
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DisplayMessageResults 
            {
                Timestamp = message.Message.Timestamp,
                EventType = "System",
                Name = "Received Settings",
                State = "OK",                
               // Information = message.Message.Message
                 
            });

            eventAggregator.PublishOnUIThread(new ConfigResults
            {
                MachineConfiguration = message.Message.MachineConfiguration
            });

        }

    }
}
