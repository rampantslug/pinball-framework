
using System.Threading.Tasks;
using Caliburn.Micro;
using MassTransit;
using MessageContracts;
using ServerLibrary.Events;

namespace ServerLibrary.ContractImplementations
{
    class CommandDeviceMessageConsumer :
        IConsumer<ICommandSwitchMessage>,
        IConsumer<ICommandCoilMessage>,
        IConsumer<ICommandStepperMotorMessage>,
        IConsumer<ICommandServoMessage>,
        IConsumer<ICommandLedMessage>

    {
        public async Task Consume(ConsumeContext<ICommandSwitchMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            await eventAggregator.PublishOnUIThreadAsync(new SwitchCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                Command = message.Message.Command
            });
        }

        public async Task Consume(ConsumeContext<ICommandCoilMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            await eventAggregator.PublishOnUIThreadAsync(new CoilCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                Command = message.Message.Command
            });
        }

        public async Task Consume(ConsumeContext<ICommandStepperMotorMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            await eventAggregator.PublishOnUIThreadAsync(new StepperMotorCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                Command = message.Message.Command
            });
        }

        public async Task Consume(ConsumeContext<ICommandServoMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            await eventAggregator.PublishOnUIThreadAsync(new ServoCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                Command = message.Message.Command
            });
        }

        public async Task Consume(ConsumeContext<ICommandLedMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            await eventAggregator.PublishOnUIThreadAsync(new LedCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                Command = message.Message.Command
            });
        }

    }
}
