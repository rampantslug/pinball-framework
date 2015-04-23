using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    public class Switch: Device, IDevice
    {

        public SwitchType Type{ get; set; }

        


        public Switch()
        {
            State = "Open";
            Type = SwitchType.NO;
        }


       // public override void UpdateNumberFromAddress()
       // {
       //     Number = (ushort)parse_matrix_num(Address);
       // }

        public override bool IsDeviceActive
        {
            get
            {
                return string.Equals(State, "Closed");
            }
        }

        private static int parse_matrix_num(string num)
        {
            string[] cr_list = num.Split('/');
            return (32 + Int32.Parse(cr_list[0]) * 16 + Int32.Parse(cr_list[1]));
        }
    }

    public enum SwitchType
    {
        NO = 0,
        NC = 1
    };
}
