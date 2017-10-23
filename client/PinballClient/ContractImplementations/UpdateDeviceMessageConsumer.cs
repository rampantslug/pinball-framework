using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MassTransit;
using MessageContracts;
using PinballClient.Events;

namespace PinballClient.ContractImplementations
{
    /// <summary>
    /// 
    /// </summary>
    class UpdateDeviceMessageConsumer:
        IConsumer<IUpdateSwitchMessage>,
        IConsumer<IUpdateCoilMessage>,
        IConsumer<IUpdateStepperMotorMessage>,
        IConsumer<IUpdateServoMessage>,
        IConsumer<IUpdateLedMessage>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public async Task Consume(ConsumeContext<IUpdateSwitchMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            await eventAggregator.PublishOnUIThreadAsync(new UpdateSwitchEvent()
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device
               
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public async Task Consume(ConsumeContext<IUpdateCoilMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            await eventAggregator.PublishOnUIThreadAsync(new UpdateCoilEvent()
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device

            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public async Task Consume(ConsumeContext<IUpdateStepperMotorMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            await eventAggregator.PublishOnUIThreadAsync(new UpdateStepperMotorEvent()
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device

            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public async Task Consume(ConsumeContext<IUpdateServoMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            await eventAggregator.PublishOnUIThreadAsync(new UpdateServoEvent()
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device

            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public async Task Consume(ConsumeContext<IUpdateLedMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            await eventAggregator.PublishOnUIThreadAsync(new UpdateLedEvent()
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device

            });
        }
    }
}
