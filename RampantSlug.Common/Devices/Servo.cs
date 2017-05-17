using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    /// <summary>
    /// Represents a servo motor device in a pinball machine.
    /// </summary>
    public class Servo : Motor, IDevice
    {
        public Servo()
        {
            State = "Inactive";
        }
    }
}
