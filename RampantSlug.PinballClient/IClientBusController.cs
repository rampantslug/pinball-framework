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

        void SendDeviceConfigMessage(IDevice device);

        void SendDeviceCommandMessage(Switch device, string tempControllerMessage);

        void SendDeviceCommandMessage(Coil device, string tempControllerMessage);

        void SendDeviceCommandMessage(StepperMotor device, string tempControllerMessage);

        void SendDeviceCommandMessage(Servo device, string tempControllerMessage);


        void RequestSettings();

        void Stop() ;
    }
}
