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

        void SendDeviceCommandMessage(IDevice device, string tempControllerMessage);

        void RequestSettings();

        void Stop() ;
    }
}
