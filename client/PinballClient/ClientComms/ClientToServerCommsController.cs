using System;
using System.ComponentModel.Composition;
using BusinessObjects.Devices;
using Common;
using Common.Commands;
using MassTransit;
using PinballClient.ContractImplementations;
using RampantSlug.PinballClient.ContractImplementations;

namespace PinballClient.ClientComms
{
    /// <summary>
    /// Controller for communication to a server using MassTransit
    /// </summary>
    [Export(typeof(IClientToServerCommsController))]
    public class ClientToServerCommsController: IClientToServerCommsController
    {
        public IClientCommsController CurrentController { get; set; }

        /// <summary>
        /// Constructor for client comms controller
        /// </summary>
        /// <param name="bus">MassTransit service bus</param>
        /// <param name="busInitializer">Common bus initializer helper</param>
        [ImportingConstructor]
        public ClientToServerCommsController(IBusControl bus, IBusInitializer busInitializer)
        {
            _bus = bus;
            _busInitializer = busInitializer;
        }

        /// <summary>
        /// Start connection to service bus on specified port
        /// </summary>
        /// <param name="serverIpAddress">IP Address of the server</param>
        public void Start(string serverIpAddress = "127.0.0.1") 
        {
            if (string.IsNullOrEmpty(serverIpAddress))
            {
                serverIpAddress = "127.0.0.1";
            }

            _bus = _busInitializer.CreateBus(serverIpAddress, "PinballClient", "pinball", "pinpass", ep => 
            { 
                ep.Consumer<LogMessageConsumer>();
                ep.Consumer<ConfigMessageConsumer>();
                ep.Consumer<UpdateDeviceMessageConsumer>();         
            });
            _bus.Start();
        }

        public void Stop()
        {
            _bus.Stop();
        }



        public void SendConfigureMachineMessage(bool useHardware)
        {
            var message = new ConfigureMachineMessage() { Timestamp = DateTime.Now, UseHardware = useHardware };
            _bus.Publish(message);
        }

        public void SendConfigureDeviceMessage(Switch device, bool removeDevice = false) 
        {
            var message = new ConfigureSwitchMessage() { Device = device , Timestamp = DateTime.Now, RemoveDevice = removeDevice};
            //_bus.Publish<ConfigureSwitchMessage>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); }); -- Need to confirm whether I want to set deliveyrMode or not?
            _bus.Publish(message);
        }

        public void SendConfigureDeviceMessage(Coil device, bool removeDevice = false)
        {
            var message = new ConfigureCoilMessage() { Device = device, Timestamp = DateTime.Now, RemoveDevice = removeDevice };
            _bus.Publish(message);
        }

        public void SendConfigureDeviceMessage(StepperMotor device, bool removeDevice = false)
        {
            var message = new ConfigureStepperMotorMessage() { Device = device, Timestamp = DateTime.Now, RemoveDevice = removeDevice };
            _bus.Publish(message);
        }

        public void SendConfigureDeviceMessage(Servo device, bool removeDevice = false)
        {
            var message = new ConfigureServoMessage() { Device = device, Timestamp = DateTime.Now, RemoveDevice = removeDevice };
            _bus.Publish(message);
        }

        public void SendConfigureDeviceMessage(Led device, bool removeDevice = false)
        {
            var message = new ConfigureLedMessage() { Device = device, Timestamp = DateTime.Now, RemoveDevice = removeDevice };
            _bus.Publish(message);
        }

        public void SendCommandDeviceMessage(Switch device, SwitchCommand command)
        {
            var message = new CommandSwitchMessage() { Device = device, Timestamp = DateTime.Now, Command = command };
            _bus.Publish(message);
        }

        public void SendCommandDeviceMessage(Coil device, CoilCommand command)
        {
            var message = new CommandCoilMessage() { Device = device, Timestamp = DateTime.Now, Command = command };
            _bus.Publish(message);
        }

        public void SendCommandDeviceMessage(StepperMotor device, StepperMotorCommand command)
        {
            var message = new CommandStepperMotorMessage() { Device = device, Timestamp = DateTime.Now, Command = command };
            _bus.Publish(message);
        }

        public void SendCommandDeviceMessage(Servo device, ServoCommand command)
        {
            var message = new CommandServoMessage() { Device = device, Timestamp = DateTime.Now, Command = command };
            _bus.Publish(message);
        }

        public void SendCommandDeviceMessage(Led device, LedCommand command)
        {
            var message = new CommandLedMessage() { Device = device, Timestamp = DateTime.Now, Command = command };
            _bus.Publish(message);
        }

        public void RestartServer()
        {
            _bus.Publish(new RestartServerMessage());
            //_bus.Publish(new RestartServerMessage(), x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void RequestSettings()
        {
            _bus.Publish(new RequestConfigMessage());
            //_bus.Publish(new RequestConfigMessage(), x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        public void RequestFile(string mediaFilename)
        {
            _bus.Publish(new RequestMediaFileMessage() {MediaFileLocation = mediaFilename});
            //_bus.Publish(new RequestMediaFileMessage(){MediaFileLocation = mediaFilename}, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });
        }

        private IBusControl _bus;
        private readonly IBusInitializer _busInitializer;
    }
}
