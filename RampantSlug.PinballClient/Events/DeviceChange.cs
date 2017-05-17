using System;
using System.Collections.Generic;
using RampantSlug.Common;
using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.Events
{
    public class DeviceChange
    {
        public DateTime Timestamp { get; set; }

        public IDevice Device { get; set; } 


    }
}
