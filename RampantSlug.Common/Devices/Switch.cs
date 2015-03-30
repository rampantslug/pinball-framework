using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    public class Switch: Device, IDevice
    {
        

        // TODO: This needs to be changed to enum of switch types
        public string SwitchType { get; set; }

        public string State { get; set; }


        public Switch() 
        {

        }

    }
}
