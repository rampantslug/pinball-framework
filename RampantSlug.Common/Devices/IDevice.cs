using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RampantSlug.Common.Devices
{
    public interface IDevice
    {
        ushort Number { get; set; }

        string Address { get; set; }

        string Name { get; set; }

        string State { get; set; }

        string InputWirePrimaryColor { get; set; }
        string InputWireSecondaryColor { get; set; }
        string OutputWirePrimaryColor { get; set; }
        string OutputWireSecondaryColor { get; set; }

        double VirtualLocationX { get; set; }

        double VirtualLocationY { get; set; }

        bool IsActive { get; }

        string RefinedType { get; set; }

    }
}
