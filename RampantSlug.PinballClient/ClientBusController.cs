using MassTransit;
using RampantSlug.Common;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient
{
    public class ClientBusController: IClientBusController
    {
        private IServiceBus _bus;


        public void Start() 
        {
            _bus = BusInitializer.CreateBus("TestClient", x => 
            { 
            x.Subscribe(subs =>
                {
                    subs.Consumer<SimpleMessageConsumer>().Permanent();
                    subs.Consumer<EventMessageConsumer>().Permanent();
                    subs.Consumer<ConfigMessageConsumer>().Permanent();
                    subs.Consumer<DeviceMessageConsumer>().Permanent();
                });
            
            
            });
          
        }

        public void SendDeviceConfigMessage(IDevice device) 
        {
            var message = new DeviceConfigMessage() { Device = device , Timestamp = DateTime.Now };
            _bus.Publish<DeviceConfigMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendDeviceCommandMessage(IDevice device, string tempControllerMessage)
        {
            var message = new DeviceCommandMessage() { Device = device, Timestamp = DateTime.Now, TempControllerMessage = tempControllerMessage};
            _bus.Publish<DeviceCommandMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void RequestSettings() 
        {
            _bus.Publish<RequestConfigMessage>(new RequestConfigMessage(), x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void Stop() { _bus.Dispose(); }
    }
}
