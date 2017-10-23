using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using Common;
using Configuration;
using Logging;
using ServerLibrary.ContractImplementations;

namespace ServerLibrary
{
    public class ServerBusController: IServerBusController
    {
        private IBusControl _bus;
        private IBusInitializer _busInitializer;

        private const string _localhost = "127.0.0.1"; // Use queue on the server for messages

        [System.ComponentModel.Composition.ImportingConstructor]
        public ServerBusController(IBusControl bus, IBusInitializer busInitializer)
        {
            _bus = bus;
            _busInitializer = busInitializer;
        }

        public void Start()
        {
            _bus = _busInitializer.CreateBus(_localhost, "PinballServer", "pinball", "pinpass", ep => 
            {

                ep.Consumer<ConfigureDeviceMessageConsumer>();
                ep.Consumer<RequestConfigMessageConsumer>();
                ep.Consumer<CommandDeviceMessageConsumer>();
                ep.Consumer<ConfigureMachineMessageConsumer>();
                ep.Consumer<RestartServerMessageConsumer>();
                ep.Consumer<RequestMediaFileMessageConsumer>();
                
            });
            _bus.Start();
        }

        public void SendLogMessage(LogEventType eventType, OriginatorType originator, string originatorName, string status, string information) 
        {
            var message = new LogMessage()
            {
                Timestamp = DateTime.Now,
                EventType = eventType,
                OriginatorType = originator,
                OriginatorName = originatorName,
                Status = status,
                Information = information
            };
            _bus.Publish<LogMessage>(message);
            //_bus.Publish<LogMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendConfigurationMessage(IMachineConfiguration configuration)
        {
            var message = new ConfigMessage() { MachineConfiguration = configuration, Timestamp = DateTime.Now };
            _bus.Publish(message);
            //_bus.Publish<ConfigMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendUpdateDeviceMessage(Switch device)
        {
            var message = new UpdateSwitchMessage() { Device = device, Timestamp = DateTime.Now };
            _bus.Publish(message);
            //_bus.Publish<UpdateSwitchMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendUpdateDeviceMessage(Coil device)
        {
            var message = new UpdateCoilMessage() { Device = device, Timestamp = DateTime.Now };
            _bus.Publish(message);
            //_bus.Publish<UpdateCoilMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendUpdateDeviceMessage(StepperMotor device)
        {
            var message = new UpdateStepperMotorMessage() { Device = device, Timestamp = DateTime.Now };
            _bus.Publish(message);
            //_bus.Publish<UpdateStepperMotorMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendUpdateDeviceMessage(Servo device)
        {
            var message = new UpdateServoMessage() { Device = device, Timestamp = DateTime.Now };
            _bus.Publish(message);
            //_bus.Publish<UpdateServoMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void SendUpdateDeviceMessage(Led device)
        {
            var message = new UpdateLedMessage() { Device = device, Timestamp = DateTime.Now };
            _bus.Publish(message);
            //_bus.Publish<UpdateLedMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void Stop()
        {
            _bus.Stop();
        }
    }
}
