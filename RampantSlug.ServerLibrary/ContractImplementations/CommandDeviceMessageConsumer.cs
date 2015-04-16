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
    class CommandDeviceMessageConsumer :
        Consumes<ICommandSwitchMessage>.Context,
        Consumes<ICommandCoilMessage>.Context,
        Consumes<ICommandStepperMotorMessage>.Context,
        Consumes<ICommandServoMessage>.Context

    {
        public void Consume(IConsumeContext<ICommandSwitchMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new SwitchCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                Command = message.Message.Command
            });
        }

        public void Consume(IConsumeContext<ICommandCoilMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new CoilCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                Command = message.Message.Command
            });
        }

        public void Consume(IConsumeContext<ICommandStepperMotorMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new StepperMotorCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                Command = message.Message.Command
            });
        }

        public void Consume(IConsumeContext<ICommandServoMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();

            eventAggregator.PublishOnUIThread(new ServoCommandResult
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device,
                Command = message.Message.Command
            });
        }

    }
}
