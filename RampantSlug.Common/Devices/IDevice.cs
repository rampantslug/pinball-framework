using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    public interface IDevice
    {
        ushort Number { get; }
        string Address { get; set; }
        string Name { get; set; }

        void UpdateNumberFromAddress();
    }
}
