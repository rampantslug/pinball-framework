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
    }
}
