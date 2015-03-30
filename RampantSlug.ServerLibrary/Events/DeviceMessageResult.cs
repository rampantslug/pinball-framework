using System;
using RampantSlug.Common.Devices;

namespace RampantSlug.ServerLibrary.Events
{
    public class DeviceMessageResult
    {
       
        public DateTime Timestamp { get; set; }

        public IDevice Device { get; set; } 
    }
}
