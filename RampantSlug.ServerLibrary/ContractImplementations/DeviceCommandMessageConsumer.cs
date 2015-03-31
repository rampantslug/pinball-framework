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
    class DeviceCommandMessageConsumer : Consumes<IDeviceCommandMessage>.Context
    {
        public void Consume(IConsumeContext<IDeviceCommandMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DeviceCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                TempControllerMessage = message.Message.TempControllerMessage

            });

        }

    }
}
