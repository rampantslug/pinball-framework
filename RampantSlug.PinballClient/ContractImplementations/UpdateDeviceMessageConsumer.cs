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
    class UpdateDeviceMessageConsumer: 
        Consumes<IUpdateSwitchMessage>.Context,
        Consumes<IUpdateCoilMessage>.Context,
        Consumes<IUpdateStepperMotorMessage>.Context,
        Consumes<IUpdateServoMessage>.Context
    {
        public void Consume(IConsumeContext<IUpdateSwitchMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DeviceChange() 
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device                 
            });
        }

        public void Consume(IConsumeContext<IUpdateCoilMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DeviceChange() 
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device                 
            });
        }

        public void Consume(IConsumeContext<IUpdateStepperMotorMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DeviceChange() 
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device                 
            });
        }

        public void Consume(IConsumeContext<IUpdateServoMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new DeviceChange() 
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device                 
            });
        }


    }
}
