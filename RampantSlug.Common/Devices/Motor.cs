using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    /// <summary>
    /// Represents a general motor device in a pinball machine.
    /// </summary>
    public class Motor : Device, IDevice
    {
        public override bool IsActive
        {
            get
            {
                return false;
            }
        }
    }
}
