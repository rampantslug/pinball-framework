using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary
{
    public class DeviceMessageResult
    {
        public DateTime Timestamp { get; set; }

        public Switch Device { get; set; }  // TODO: Change this to a higher level IDevice that switch/coil etc... inherits from
    }
}
