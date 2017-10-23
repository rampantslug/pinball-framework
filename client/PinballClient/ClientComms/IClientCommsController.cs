using BusinessObjects.Devices;
using Common.Commands;

namespace PinballClient.ClientComms
{
    public interface IClientCommsController
    {
        IClientCommsController CurrentController { get; set; }
        void SendConfigureMachineMessage(bool useHardware);

        void SendConfigureDeviceMessage(Switch device, bool removeDevice = false);

        void SendConfigureDeviceMessage(Coil device, bool removeDevice = false);

        void SendConfigureDeviceMessage(StepperMotor device, bool removeDevice = false);

        void SendConfigureDeviceMessage(Servo device, bool removeDevice = false);

        void SendConfigureDeviceMessage(Led device, bool removeDevice = false);

        void SendCommandDeviceMessage(Switch device, SwitchCommand command);

        void SendCommandDeviceMessage(Coil device, CoilCommand command);

        void SendCommandDeviceMessage(StepperMotor device, StepperMotorCommand command);

        void SendCommandDeviceMessage(Servo device, ServoCommand command);

        void SendCommandDeviceMessage(Led device, LedCommand command);

        void RequestSettings();

        void RequestFile(string mediaFilename);
        
    }
}
