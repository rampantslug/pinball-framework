using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    public class Switch:Device, IDevice
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

        // TODO: This needs to be changed to enum of switch types
        private string _switchType;
        public string SwitchType
        {
            get { return _switchType; }
            set
            {
                _switchType = value;
            }
        }

        private string _state;
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
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

        // TODO: Should this be an enum of available colours based on what is set for the project
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

        public Switch() 
        {

            Number = 1;
            Address = "01/01";
            Name = "Ball Trough 1";
            SwitchType = "Optical NC";
            State = "Inactive";
            LastChangeTimeStamp = DateTime.Now;
            WiringColors = "Grey/Green";

        }

    }
}
