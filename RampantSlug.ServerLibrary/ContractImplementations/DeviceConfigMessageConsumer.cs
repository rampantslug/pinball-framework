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
    class ConfigureDeviceMessageConsumer : 
        Consumes<IConfigureSwitchMessage>.Context,
        Consumes<IConfigureCoilMessage>.Context,
        Consumes<IConfigureStepperMotorMessage>.Context,
        Consumes<IConfigureServoMessage>.Context,
        Consumes<IConfigureLedMessage>.Context
    {
        public void Consume(IConsumeContext<IConfigureSwitchMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new ConfigureSwitchEvent()
            {
                //Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                RemoveDevice = message.Message.RemoveDevice
            });

        }

        public void Consume(IConsumeContext<IConfigureCoilMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new ConfigureCoilEvent()
            {
                //Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                RemoveDevice = message.Message.RemoveDevice
            });

        }

        public void Consume(IConsumeContext<IConfigureStepperMotorMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new ConfigureStepperMotorEvent()
            {
                //Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                RemoveDevice = message.Message.RemoveDevice
            });

        }

        public void Consume(IConsumeContext<IConfigureServoMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new ConfigureServoEvent()
            {
                //Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                RemoveDevice = message.Message.RemoveDevice
            });

        }

        public void Consume(IConsumeContext<IConfigureLedMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new ConfigureLedEvent()
            {
                //Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                RemoveDevice = message.Message.RemoveDevice
            });

        }

    }
}
