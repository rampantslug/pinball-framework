using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    /// <summary>
    /// Represents a switch device in a pinball machine.
    /// </summary>
    public class Switch: Device, IDevice
    {
        public SwitchType Type{ get; set; }

        public string InputWirePrimaryColor { get; set; }
        public string InputWireSecondaryColor { get; set; }
        public string OutputWirePrimaryColor { get; set; }
        public string OutputWireSecondaryColor { get; set; }


        public Switch()
        {
            State = "Open";
            Type = SwitchType.NO;
        }

        public override bool IsActive
        {
            get
            {
                return string.Equals(State, "Closed");
            }
        }

    }

    public enum SwitchType
    {
        NO = 0, // Normally Open 
        NC = 1 // Normally Closed (Optos)
    };
}
