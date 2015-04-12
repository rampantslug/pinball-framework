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
                    subs.Consumer<SimpleMessageConsumer>().Transient();
                    subs.Consumer<EventMessageConsumer>().Transient();
                    subs.Consumer<ConfigMessageConsumer>().Transient();
                    subs.Consumer<DeviceMessageConsumer>().Transient();
                });
            
            
            });
          
        }

        public void SendDeviceConfigMessage(IDevice device) 
        {
            var message = new DeviceConfigMessage() { Device = device , Timestamp = DateTime.Now };
            _bus.Publish<DeviceConfigMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendDeviceCommandMessage(Switch device, string tempControllerMessage)
        {
            var message = new SwitchCommandMessage() { Device = device, Timestamp = DateTime.Now, TempControllerMessage = tempControllerMessage};
            _bus.Publish<SwitchCommandMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendDeviceCommandMessage(Coil device, string tempControllerMessage)
        {
            var message = new CoilCommandMessage() { Device = device, Timestamp = DateTime.Now, TempControllerMessage = tempControllerMessage };
            _bus.Publish<CoilCommandMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendDeviceCommandMessage(StepperMotor device, string tempControllerMessage)
        {
            var message = new StepperMotorCommandMessage() { Device = device, Timestamp = DateTime.Now, TempControllerMessage = tempControllerMessage };
            _bus.Publish<StepperMotorCommandMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendDeviceCommandMessage(Servo device, string tempControllerMessage)
        {
            var message = new ServoCommandMessage() { Device = device, Timestamp = DateTime.Now, TempControllerMessage = tempControllerMessage };
            _bus.Publish<ServoCommandMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void RequestSettings() 
        {
            _bus.Publish<RequestConfigMessage>(new RequestConfigMessage(), x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void Stop() { _bus.Dispose(); }
    }
}
