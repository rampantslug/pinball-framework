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
    class DeviceCommandMessageConsumer : Consumes<ISwitchCommandMessage>.Context, 
        Consumes<ICoilCommandMessage>.Context,
        Consumes<IStepperMotorCommandMessage>.Context,
        Consumes<IServoCommandMessage>.Context

    {
        public void Consume(IConsumeContext<ISwitchCommandMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DeviceCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                TempControllerMessage = message.Message.TempControllerMessage
            });
        }

        public void Consume(IConsumeContext<ICoilCommandMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DeviceCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                TempControllerMessage = message.Message.TempControllerMessage
            });
        }

        public void Consume(IConsumeContext<IStepperMotorCommandMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DeviceCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                TempControllerMessage = message.Message.TempControllerMessage
            });
        }

        public void Consume(IConsumeContext<IServoCommandMessage> message)
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
