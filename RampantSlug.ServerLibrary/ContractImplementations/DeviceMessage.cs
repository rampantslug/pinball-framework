using RampantSlug.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common.Devices;

namespace RampantSlug.ServerLibrary.ContractImplementations
{
    class DeviceMessage: IDeviceMessage
    {
        public DateTime Timestamp { get; set; }

        public IDevice Device { get; set; }
    }
}
