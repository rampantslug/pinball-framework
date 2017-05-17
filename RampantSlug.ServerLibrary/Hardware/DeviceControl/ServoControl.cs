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
    public class ServoControl : Servo
    {
        public Servo BaseServo { get; set; }
        
        public ServoControl(Servo servo)
        {
            BaseServo = servo;

           // Initialise all values from servo
            Number = servo.Number;
            Address = servo.Address;
            Name = servo.Name;
            State = servo.State;
            LastChangeTimeStamp = servo.LastChangeTimeStamp;
            //WiringColors = servo.WiringColors;
            VirtualLocationX = servo.VirtualLocationX;
            VirtualLocationY = servo.VirtualLocationY;
        }

        public void Rotate()
        {

        }

    }
}
