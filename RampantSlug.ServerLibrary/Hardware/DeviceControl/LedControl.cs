using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using RampantSlug.Common.Devices;
using RampantSlug.ServerLibrary.Events;

namespace RampantSlug.ServerLibrary.Hardware.DeviceControl
{
    public class LedControl : Led
    {
        public Led BaseLed { get; set; }

        public LedControl(Led led)
        {
            BaseLed = led;

           // Initialise all values from servo
            Number = led.Number;
            Address = led.Address;
            Name = led.Name;
            State = led.State;
            LastChangeTimeStamp = led.LastChangeTimeStamp;
            //WiringColors = led.WiringColors;
            VirtualLocationX = led.VirtualLocationX;
            VirtualLocationY = led.VirtualLocationY;
        }

        public void MidIntensityOn()
        {

        }

    }
}
