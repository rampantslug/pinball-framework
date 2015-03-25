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
    class RequestSettingsMessageConsumer : Consumes<IRequestSettingsMessage>.Context
    {
        public void Consume(IConsumeContext<IRequestSettingsMessage> message)
        {
            //TODO: Do we need access to the configuration or general 'game' info at this point rather than marshalling to the event Aggregator
            
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new RequestSettingsResult());

        }

    }
}
