using Caliburn.Micro;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageContracts;
using ServerLibrary.Events;

namespace ServerLibrary.ContractImplementations
{
    class ConfigureDeviceMessageConsumer :
        IConsumer<IConfigureSwitchMessage>,
        IConsumer<IConfigureCoilMessage>,
        IConsumer<IConfigureStepperMotorMessage>,
        IConsumer<IConfigureServoMessage>,
        IConsumer<IConfigureLedMessage>
    {
        public async Task Consume(ConsumeContext<IConfigureSwitchMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            await eventAggregator.PublishOnUIThreadAsync(new ConfigureSwitchEvent()
            {
                //Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                RemoveDevice = message.Message.RemoveDevice
            });

        }

        public async Task Consume(ConsumeContext<IConfigureCoilMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            await eventAggregator.PublishOnUIThreadAsync(new ConfigureCoilEvent()
            {
                //Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                RemoveDevice = message.Message.RemoveDevice
            });

        }

        public async Task Consume(ConsumeContext<IConfigureStepperMotorMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            await eventAggregator.PublishOnUIThreadAsync(new ConfigureStepperMotorEvent()
            {
                //Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                RemoveDevice = message.Message.RemoveDevice
            });

        }

        public async Task Consume(ConsumeContext<IConfigureServoMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            await eventAggregator.PublishOnUIThreadAsync(new ConfigureServoEvent()
            {
                //Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                RemoveDevice = message.Message.RemoveDevice
            });

        }

        public async Task Consume(ConsumeContext<IConfigureLedMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            await eventAggregator.PublishOnUIThreadAsync(new ConfigureLedEvent()
            {
                //Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                RemoveDevice = message.Message.RemoveDevice
            });

        }

    }
}
