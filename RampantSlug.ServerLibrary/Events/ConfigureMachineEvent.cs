using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common.Devices;

namespace RampantSlug.ServerLibrary.Events
{
    public class ConfigureMachineEvent
    {
        public bool UseHardware { get; set; }
    }
}
