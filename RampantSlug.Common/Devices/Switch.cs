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
            State = "Open";
        }


        public override void UpdateNumberFromAddress()
        {
            Number = (ushort)parse_matrix_num(Address);
        }

        private static int parse_matrix_num(string num)
        {
            string[] cr_list = num.Split('/');
            return (32 + Int32.Parse(cr_list[0]) * 16 + Int32.Parse(cr_list[1]));
        }
    }
}
