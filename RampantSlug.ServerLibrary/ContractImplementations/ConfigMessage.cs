using RampantSlug.Common.Devices;
using RampantSlug.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;

namespace RampantSlug.ServerLibrary.ContractImplementations
{
    class ConfigMessage: IConfigMessage
    {
        public DateTime Timestamp { get; set; }

        public Configuration MachineConfiguration{ get; set; }
    }
}
