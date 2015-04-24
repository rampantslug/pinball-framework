using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common.Commands;

namespace RampantSlug.PinballClient
{
    public interface IClientBusController
    {
        void Start();

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

        void Stop() ;
    }
}
