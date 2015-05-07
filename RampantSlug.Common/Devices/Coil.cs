using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    /// <summary>
    /// Represents a coil device in a pinball machine.
    /// </summary>
    public class Coil: Device, IDevice
    {

        public Coil()
        {
            State = "Inactive";      
        }

        public override bool IsActive
        {
            get
            {
                return false;
            }
        }
    }
}
