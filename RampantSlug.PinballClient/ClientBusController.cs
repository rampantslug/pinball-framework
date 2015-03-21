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
                    subs.Consumer<SettingsMessageConsumer>().Permanent();
                });
            
            
            });
          
        }

        public void SendDeviceMessage(Switch device) 
        {
            var message = new DeviceMessage() { Device = device , Timestamp = DateTime.Now };
            _bus.Publish<DeviceMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void RequestSettings() 
        {
            _bus.Publish<RequestSettingsMessage>(new RequestSettingsMessage(), x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void Stop() { _bus.Dispose(); }
    }
}
