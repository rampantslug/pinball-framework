using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    public class Motor : Device, IDevice
    {

        public override void UpdateNumberFromAddress()
        {
            Number = ushort.Parse(Address);
        }
    }
}
