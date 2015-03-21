using RampantSlug.Common.Devices;
using RampantSlug.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary.ContractImplementations
{
    class SettingsMessage: ISettingsMessage
    {
        public List<Switch> Switches { get; set; }
    }
}
