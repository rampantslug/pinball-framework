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
    class SimpleMessageConsumer: Consumes<ISimpleMessage>.Context
    {
        public void Consume(IConsumeContext<ISimpleMessage> message)
        {
            //var eventAggregator = IoC.Get<IEventAggregator>();

            TempData.SomeText = message.Message.Message;
        }

    }
}
