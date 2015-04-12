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
        //[JsonIgnore]
        ushort Number { get; set; }

        string Address { get; set; }
        string Name { get; set; }

       // void UpdateNumberFromAddress();

        

        [JsonIgnore]
        bool IsDeviceActive { get; }
    }
}
