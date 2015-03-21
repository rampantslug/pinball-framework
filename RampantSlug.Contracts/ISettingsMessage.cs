using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Contracts
{
    public interface ISettingsMessage
    {
  
        List<Switch> Switches { get; }  // TODO: Change this to a higher level IDevice that switch/coil etc... inherits from
    }
}
