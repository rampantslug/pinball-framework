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

    public class CoilType : DeviceType
    {
        public CoilType(string deviceTypeName)
            : base(deviceTypeName)
        {
        }

        readonly List<Coil> _coils = new List<Coil>();
        public List<Coil> Coils
        {
            get { return _coils; }
        }
    }

    public class ServoType : DeviceType
    {
        public ServoType(string deviceTypeName)
            : base(deviceTypeName)
        {
        }

        readonly List<Servo> _servos = new List<Servo>();
        public List<Servo> Servos
        {
            get { return _servos; }
        }
    }

    public class StepperMotorType : DeviceType
    {
        public StepperMotorType(string deviceTypeName)
            : base(deviceTypeName)
        {
        }

        readonly List<StepperMotor> _stepperMotors = new List<StepperMotor>();
        public List<StepperMotor> StepperMotors
        {
            get { return _stepperMotors; }
        }
    }

    public class DCMotorType : DeviceType
    {
        public DCMotorType(string deviceTypeName)
            : base(deviceTypeName)
        {
        }

        readonly List<DCMotor> _dcMotors = new List<DCMotor>();
        public List<DCMotor> DCMotors
        {
            get { return _dcMotors; }
        }
    }

    public class LedType : DeviceType
    {
        public LedType(string deviceTypeName)
            : base(deviceTypeName)
        {
        }

        readonly List<Led> _leds = new List<Led>();
        public List<Led> Leds
        {
            get { return _leds; }
        }
    }

}
