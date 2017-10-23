using Caliburn.Micro;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageContracts;
using PinballClient.Events;

namespace PinballClient.ContractImplementations
{
    /// <summary>
    /// 
    /// </summary>
    class LogMessageConsumer: IConsumer<ILogMessage>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public async Task Consume(ConsumeContext<ILogMessage> message)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            await eventAggregator.PublishOnUIThreadAsync(new LogEvent 
            {
                Timestamp = message.Message.Timestamp,
                EventType = message.Message.EventType,
                OriginatorType = message.Message.OriginatorType,
                OriginatorName = message.Message.OriginatorName,
                Status = message.Message.Status,                
                Information = message.Message.Information                 
            });

        }
    }
}
