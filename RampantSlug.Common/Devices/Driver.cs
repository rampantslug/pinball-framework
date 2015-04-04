using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    public class Driver : Device
    {
        public override void UpdateNumberFromAddress()
        {
            Number = ushort.Parse(Address);
        }
    }
}