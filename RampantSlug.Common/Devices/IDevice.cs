using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    public interface IDevice
    {
        string Description { get; set; }
        string Name { get; set; }
    }
}
