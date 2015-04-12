using MassTransit;
using RampantSlug.Common;
using RampantSlug.Common.Devices;
using RampantSlug.ServerLibrary.ContractImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary
{
    public class ServerBusController: IServerBusController
    {
        private IServiceBus _bus;


        public void Start() 
        {
            _bus = BusInitializer.CreateBus("TestServer", x => 
            {
                x.Subscribe(subs =>
                {
                    subs.Consumer<DeviceConfigMessageConsumer>().Transient();
                    subs.Consumer<RequestConfigMessageConsumer>().Transient();
                    subs.Consumer<DeviceCommandMessageConsumer>().Transient();
                });
            });
          
        }

        public void SendEventMessage(string text) 
        {
            var message = new EventMessage() { Message = text , Timestamp = DateTime.Now };
            _bus.Publish<EventMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendSimpleMessage(string text)
        {
            var message = new SimpleMessage() { Message = text, Timestamp = DateTime.Now };
            _bus.Publish<SimpleMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendConfigurationMessage(Configuration configuration)
        {
            var message = new ConfigMessage() { MachineConfiguration = configuration, Timestamp = DateTime.Now };
            _bus.Publish<ConfigMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendDeviceMessage(IDevice device)
        {
            var message = new DeviceMessage() { Device = device, Timestamp = DateTime.Now };
            _bus.Publish<DeviceMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void Stop() { _bus.Dispose(); }
    }
}
