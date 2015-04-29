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
    public class CoilControl : Coil
    {
        public Coil BaseCoil { get; set; }

        public CoilControl(Coil coil)
        {
            BaseCoil = coil;

           // Initialise all values from servo
            Number = coil.Number;
            Address = coil.Address;
            Name = coil.Name;
            State = coil.State;
            LastChangeTimeStamp = coil.LastChangeTimeStamp;
            WiringColors = coil.WiringColors;
            VirtualLocationX = coil.VirtualLocationX;
            VirtualLocationY = coil.VirtualLocationY;
        }

        public void Pulse()
        {

        }

    }
}
