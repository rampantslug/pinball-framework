using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree
{
    public class DeviceType
    {
        public DeviceType(string deviceTypeName)
        {
            DeviceTypeName = deviceTypeName;
        }

        public string DeviceTypeName { get; private set; }


    }

    public class CoilType : DeviceType
    {
        public CoilType(string deviceTypeName)
            : base(deviceTypeName)
        {
        }

        readonly List<Driver> _coils = new List<Driver>();
        public List<Driver> Coils
        {
            get { return _coils; }
        }
    }

    public class SwitchType : DeviceType
    {
        public SwitchType(string deviceTypeName)
            : base(deviceTypeName)
        {
        }

        readonly List<Switch> _switches = new List<Switch>();
        public List<Switch> Switches
        {
            get { return _switches; }
        }
    }
}
