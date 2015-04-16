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

        void SendConfigureDeviceMessage(Switch device);

        void SendConfigureDeviceMessage(Coil device);

        void SendConfigureDeviceMessage(StepperMotor device);

        void SendConfigureDeviceMessage(Servo device);

        void SendCommandDeviceMessage(Switch device, SwitchCommand command);

        void SendCommandDeviceMessage(Coil device, CoilCommand command);

        void SendCommandDeviceMessage(StepperMotor device, StepperMotorCommand command);

        void SendCommandDeviceMessage(Servo device, ServoCommand command);


        void RequestSettings();

        void Stop() ;
    }
}
