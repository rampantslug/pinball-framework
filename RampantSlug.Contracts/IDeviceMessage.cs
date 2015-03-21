using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Contracts
{
    public interface IDeviceMessage
    {
        
        DateTime Timestamp { get; }

        Switch Device { get; }  // TODO: Change this to a higher level IDevice that switch/coil etc... inherits from
    }
}
