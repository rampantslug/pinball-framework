
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;


namespace Common
{
    public class BusInitializer : IBusInitializer
    {
        /// <summary>
        /// Create a Service Bus.
        /// </summary>
        /// <param name="queueIpAddress"></param>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="userName">Username for user on RabbitMq.</param>
        /// <param name="userPassword">Password for user on RabbitMq.</param>
        /// <param name="moreInitialization">Initialisation to perform on creation.</param>        
        /// <returns>New Service Bus.</returns>
        public IBusControl CreateBus(string queueIpAddress, string queueName, string userName, string userPassword, Action<IRabbitMqReceiveEndpointConfigurator> moreInitialization)
        {
            //Log4NetLogger.Use();
            var busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(
                    new Uri($"rabbitmq://{queueIpAddress}"),
                    h =>
                    {
                        h.Username(userName);
                        h.Password(userPassword);
                        //h.Username("guest");
                        //h.Password("guest");
                    });
                sbc.UseRetry(Retry.Immediate(5));

                sbc.ReceiveEndpoint(host, @"RampantSlug.PinballFramework_" + queueName, moreInitialization);
                sbc.AutoDelete = false;
            });
                      
            return busControl;
        }
    }

    public interface IBusInitializer
    {
            IBusControl CreateBus(string queueIpAddress, string queueName, string userName, string userPassword, Action<IRabbitMqReceiveEndpointConfigurator> moreInitialization);
    }
}
