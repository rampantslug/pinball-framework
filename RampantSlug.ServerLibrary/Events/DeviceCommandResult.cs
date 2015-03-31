using System;
using RampantSlug.Common.Devices;

namespace RampantSlug.ServerLibrary.Events
{
    public class DeviceCommandResult
    {
        public DateTime Timestamp { get; set; }

        public IDevice Device { get; set; }

        public string TempControllerMessage { get; set; }
    }
}
