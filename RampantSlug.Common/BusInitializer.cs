using MassTransit;
using MassTransit.BusConfigurators;
using MassTransit.Log4NetIntegration.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common
{
    public class BusInitializer
    {
        /// <summary>
        /// Create a Service Bus.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="moreInitialization">Initialisation to perform on creation.</param>
        /// <returns>New Service Bus.</returns>
        public static IServiceBus CreateBus(string queueName, Action<ServiceBusConfigurator> moreInitialization)
        {
            Log4NetLogger.Use();
            var bus = ServiceBusFactory.New(x =>
            {
                x.UseRabbitMq();
                x.ReceiveFrom("rabbitmq://localhost/RampantSlug.PinballFramework_" + queueName);
                x.SetPurgeOnStartup(true);
                moreInitialization(x);
            });

            return bus;
        }
    }
}
