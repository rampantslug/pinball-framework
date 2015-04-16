using System;
using System.Windows.Media;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.Events
{
    public class UpdateSwitch
    {
        public DateTime Timestamp { get; set; }

        public Switch Device { get; set; } 
    }
}
