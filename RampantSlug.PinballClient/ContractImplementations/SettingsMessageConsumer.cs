using Caliburn.Micro;
using MassTransit;
using RampantSlug.Contracts;
using RampantSlug.PinballClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ContractImplementations
{
    class SettingsMessageConsumer: Consumes<ISettingsMessage>.Context
    {
        public void Consume(IConsumeContext<ISettingsMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DisplayMessageResults 
            {
                //Timestamp = message.Message.Timestamp,
                EventType = "System",
                Name = "Received Settings",
                State = "OK",                
               // Information = message.Message.Message
                 
            });

            eventAggregator.PublishOnUIThread(new SettingsResults
            {
                Switches = message.Message.Switches
            });

        }

    }
}
