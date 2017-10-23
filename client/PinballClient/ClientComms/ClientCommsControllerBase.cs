using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using Common.Commands;

namespace PinballClient.ClientComms
{
    [Export(typeof(IClientCommsController))]
    public class ClientCommsControllerBase : IClientCommsController
    {
        public IClientCommsController CurrentController { get; set; }

        [ImportingConstructor]
        public ClientCommsControllerBase(IClientToLocalCommsController clientToLocalCommsController)
        {
            // Use LocalController as default
            CurrentController = clientToLocalCommsController;
        }

        public void SendConfigureMachineMessage(bool useHardware)
        {
            CurrentController.SendConfigureMachineMessage(useHardware);
        }

        public void SendConfigureDeviceMessage(Switch device, bool removeDevice = false)
        {
            CurrentController.SendConfigureDeviceMessage(device, removeDevice);
        }

        public void SendConfigureDeviceMessage(Coil device, bool removeDevice = false)
        {
            CurrentController.SendConfigureDeviceMessage(device, removeDevice);
        }

        public void SendConfigureDeviceMessage(StepperMotor device, bool removeDevice = false)
        {
            CurrentController.SendConfigureDeviceMessage(device, removeDevice);
        }

        public void SendConfigureDeviceMessage(Servo device, bool removeDevice = false)
        {
            CurrentController.SendConfigureDeviceMessage(device, removeDevice);
        }

        public void SendConfigureDeviceMessage(Led device, bool removeDevice = false)
        {
            CurrentController.SendConfigureDeviceMessage(device, removeDevice);
        }

        public void SendCommandDeviceMessage(Switch device, SwitchCommand command)
        {
            CurrentController.SendCommandDeviceMessage(device, command);
        }

        public void SendCommandDeviceMessage(Coil device, CoilCommand command)
        {
            CurrentController.SendCommandDeviceMessage(device, command);
        }

        public void SendCommandDeviceMessage(StepperMotor device, StepperMotorCommand command)
        {
            CurrentController.SendCommandDeviceMessage(device, command);
        }

        public void SendCommandDeviceMessage(Servo device, ServoCommand command)
        {
            CurrentController.SendCommandDeviceMessage(device, command);
        }

        public void SendCommandDeviceMessage(Led device, LedCommand command)
        {
            CurrentController.SendCommandDeviceMessage(device, command);
        }

        public void RequestSettings()
        {
            CurrentController.RequestSettings();
        }

        public void RequestFile(string mediaFilename)
        {
            CurrentController.RequestFile(mediaFilename);
        }
    }
}
