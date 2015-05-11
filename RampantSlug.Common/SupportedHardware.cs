using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common
{
    public static class SupportedHardware
    {
        public const string ProcSwitchMatrix = "PSM";
        public const string ProcDirectSwitch = "PDS";
        public const string ProcDriverBoard = "PDB";
        public const string ProcLedBoard = "PLB";
        public const string ArduinoServoShield = "ASS";
        public const string ArduinoMotorShield = "AMS";


        public static Dictionary<string, string> SwitchHardware = new Dictionary<string, string>()
        {
            {ProcSwitchMatrix,"Proc Switch Matrix"},
            {ProcDirectSwitch,"Proc Direct Switch"},
        };

        public static Dictionary<string, string> MotorHardware = new Dictionary<string, string>()
        {
            {ArduinoServoShield,"Arduino Servo Shield"},
            {ArduinoMotorShield,"Arduino Motor Shield"},
        };

        public static Dictionary<string, string> CoilHardware = new Dictionary<string, string>()
        {
            {ProcDriverBoard,"Proc Driver Board"}
        };

        public static Dictionary<string, string> LedHardware = new Dictionary<string, string>()
        {
            {ProcLedBoard,"Proc Led Board"}
        };

        public static bool IsAddressOfHardwareType(string hardware, string address)
        {
            // Split out address into Controller and port
            var controllerPlusPort = address.Split('-');

            if (controllerPlusPort.Count() != 2)
            {
                return false;
            }
            return string.Equals(controllerPlusPort[0], hardware);
        }

     /*   public static ushort GetColumn(string address)
        {
            var temp = new Address(address);
        }

        public static ushort GetRow(string address)
        {

        }*/


        private static int ParseAddressString(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return -1;
            }

            // Split out address into Controller and port
            var controllerPlusPort = address.Split('-');

            if (controllerPlusPort.Count() != 2)
            {
                // Invalid Address
                // Throw exception, error message and dont process the rest of this Device
                return -1;
            }

            switch (controllerPlusPort[0])
            {
                // Proc Switch Matrix Address
                case ProcSwitchMatrix:
                    {
                        return parse_matrix_num(controllerPlusPort[1]);
                    }

                // Proc Direct Switch Address
                case ProcDirectSwitch:
                    {
                        return Int32.Parse(controllerPlusPort[1]);
                    }

                // Proc Driver Board (Coil?) Address
                case ProcDriverBoard:
                    {
                        return Int32.Parse(controllerPlusPort[1]);
                    }

                // Proc Led Board Address
                case ProcLedBoard:
                    {
                        return Int32.Parse(controllerPlusPort[1]);
                    }

                // Arduino Servo Shield Address
                case ArduinoServoShield:
                    {
                        return Int32.Parse(controllerPlusPort[1]);
                    }

                // Arduino Motor Shield Address
                case ArduinoMotorShield:
                    {
                        return Int32.Parse(controllerPlusPort[1]);
                    }
                default:
                    {
                        return -1;
                    }
            }

        }

        private static int parse_matrix_num(string num)
        {
            string[] cr_list = num.Split('/');
            return (32 + Int32.Parse(cr_list[0]) * 16 + Int32.Parse(cr_list[1]));
        }


    }
}
