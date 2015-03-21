using Caliburn.Micro;
using MassTransit;
using RampantSlug.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary.ContractImplementations
{
    class DeviceMessageConsumer : Consumes<IDeviceMessage>.Context
    {
        public void Consume(IConsumeContext<IDeviceMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DeviceMessageResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device

            });

        }

    }
}
