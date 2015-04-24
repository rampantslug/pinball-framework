using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;

namespace RampantSlug.Contracts
{
    /// <summary>
    /// Message containing configuration data of a machine.
    /// </summary>
    public interface IConfigureMachineMessage
    {
        DateTime Timestamp { get; }

        bool UseHardware { get; set; }
    }
}
