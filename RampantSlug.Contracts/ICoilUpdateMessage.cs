using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Contracts
{
    public interface ICoilUpdateMessage
    {        
        DateTime Timestamp { get; }

        Coil Device { get; } 
    }
}
