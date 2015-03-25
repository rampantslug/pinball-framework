using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    public class Coil: Device, IDevice
    {
        #region Properties

        private int _number;
        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        private DateTime _lastChangeTimeStamp;
        public DateTime LastChangeTimeStamp
        {
            get { return _lastChangeTimeStamp; }
            set
            {
                _lastChangeTimeStamp = value;
            }
        }
        private string _wiringColors;
        public string WiringColors
        {
            get { return _wiringColors; }
            set
            {
                _wiringColors = value;
            }
        }

        #endregion

        public Coil() { }

    }
}
