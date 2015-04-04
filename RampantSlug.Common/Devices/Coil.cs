using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    public class Coil: Device, IDevice
    {

        public Coil()
        {
        }

        public override void UpdateNumberFromAddress()
        {
            Number = ushort.Parse(Address);
        }
    }
}
