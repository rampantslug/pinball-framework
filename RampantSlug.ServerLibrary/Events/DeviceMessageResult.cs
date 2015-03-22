using System;
using RampantSlug.Common.Devices;

namespace RampantSlug.ServerLibrary.Events
{
    public class DeviceMessageResult
    {
        
        
        
        public DateTime Timestamp { get; set; }

        public Switch Device { get; set; }  // TODO: Change this to a higher level IDevice that switch/coil etc... inherits from
    }
}
