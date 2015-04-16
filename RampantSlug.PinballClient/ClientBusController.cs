using MassTransit;
using RampantSlug.Common;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common.Commands;

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
                    subs.Consumer<UpdateDeviceMessageConsumer>().Transient();
                });
            
            
            });
          
        }

        public void SendConfigureDeviceMessage(Switch device) 
        {
            var message = new ConfigureSwitchMessage() { Device = device , Timestamp = DateTime.Now };
            _bus.Publish<ConfigureSwitchMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendConfigureDeviceMessage(Coil device)
        {
            var message = new ConfigureCoilMessage() { Device = device, Timestamp = DateTime.Now };
            _bus.Publish<ConfigureCoilMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendConfigureDeviceMessage(StepperMotor device)
        {
            var message = new ConfigureStepperMotorMessage() { Device = device, Timestamp = DateTime.Now };
            _bus.Publish<ConfigureStepperMotorMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendConfigureDeviceMessage(Servo device)
        {
            var message = new ConfigureServoMessage() { Device = device, Timestamp = DateTime.Now };
            _bus.Publish<ConfigureServoMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendCommandDeviceMessage(Switch device, SwitchCommand command)
        {
            var message = new CommandSwitchMessage() { Device = device, Timestamp = DateTime.Now, Command = command };
            _bus.Publish<CommandSwitchMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendCommandDeviceMessage(Coil device, CoilCommand command)
        {
            var message = new CommandCoilMessage() { Device = device, Timestamp = DateTime.Now, Command = command };
            _bus.Publish<CommandCoilMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendCommandDeviceMessage(StepperMotor device, StepperMotorCommand command)
        {
            var message = new CommandStepperMotorMessage() { Device = device, Timestamp = DateTime.Now, Command = command };
            _bus.Publish<CommandStepperMotorMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendCommandDeviceMessage(Servo device, ServoCommand command)
        {
            var message = new CommandServoMessage() { Device = device, Timestamp = DateTime.Now, Command = command };
            _bus.Publish<CommandServoMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void RequestSettings() 
        {
            _bus.Publish<RequestConfigMessage>(new RequestConfigMessage(), x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void Stop() { _bus.Dispose(); }
    }
}
