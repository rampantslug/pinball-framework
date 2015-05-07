using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    /// <summary>
    /// Represents a led device in a pinball machine.
    /// </summary>
    public class Led : Device, IDevice
    {

        public Led()
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
