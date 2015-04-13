using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient
{
    public interface IClientBusController
    {
        void Start();

        void SendConfigureDeviceMessage(Switch device);

        void SendConfigureDeviceMessage(Coil device);

        void SendConfigureDeviceMessage(StepperMotor device);

        void SendConfigureDeviceMessage(Servo device);

        void SendCommandDeviceMessage(Switch device, string tempControllerMessage);

        void SendCommandDeviceMessage(Coil device, string tempControllerMessage);

        void SendCommandDeviceMessage(StepperMotor device, string tempControllerMessage);

        void SendCommandDeviceMessage(Servo device, string tempControllerMessage);


        void RequestSettings();

        void Stop() ;
    }
}
