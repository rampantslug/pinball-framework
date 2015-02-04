using MassTransit;
using RampantSlug.Common;
using RampantSlug.ServerLibrary.ContractImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary
{
    public class BusController: IBusController
    {
        private IServiceBus _bus;


        public void Start() 
        {
            _bus = BusInitializer.CreateBus("TestPublisher", x => { });
          
        }

        public void SendText(string text) 
        {
            var message = new SimpleMessage() { Message = text, Timestamp = DateTime.Now };
            _bus.Publish<SimpleMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void Stop() { _bus.Dispose(); }
    }
}
