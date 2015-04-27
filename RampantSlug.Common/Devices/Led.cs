using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    public class Led : Device, IDevice
    {
        //public override void UpdateNumberFromAddress()
        //{
        
        //}

        public Led()
        {
            State = "Inactive";
        }

        public override bool IsDeviceActive
        {
            get
            {
                return false;
            }
        }
    }
}
