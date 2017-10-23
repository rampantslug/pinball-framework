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
    class ConfigMessageConsumer: IConsumer<IConfigMessage>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public async Task Consume(ConsumeContext<IConfigMessage> message)
        {    
            var eventAggregator = IoC.Get<IEventAggregator>();
            await eventAggregator.PublishOnUIThreadAsync(new UpdateConfigEvent()
            {
                MachineConfiguration = message.Message.MachineConfiguration
            });           
        }

    }
}
