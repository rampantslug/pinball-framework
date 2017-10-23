using System;
using System.Linq;
using System.Reflection;

namespace Hardware.DeviceAddress
{
    public static class AddressFactory
    {
        public enum HardwareType
        {
            ProcSwitchMatrix,
            ProcDirectSwitch,
            ProcDriverBoard,
            ProcLedBoard,
            ArduinoServoShield,
            ArduinoMotorShield,
            UnknownHardware
        }


        public static IAddress CreateAddress(string addressString)
        {

            if (string.IsNullOrEmpty(addressString))
            {
                return DefaultAddress();
            }

            // Split out address into Controller and port
            var controllerPlusPort = addressString.Split('-');
            if (controllerPlusPort.Count() != 2)
            {
                return DefaultAddress();
            }

            var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.GetInterfaces().Contains(typeof(IAddress)) && t.GetConstructor(Type.EmptyTypes) != null
                select Activator.CreateInstance((Type) t) as IAddress;

            foreach (var instance in instances)
            {
                if (instance.HardwareMatchesThisAddress(addressString))
                {
                    instance.UpdateAddressString(addressString);
                    return instance;
                }
            }

            return DefaultAddress();         
        }

        private static IAddress DefaultAddress()
        {
            var address = new ZzzAddress();
            return address;
        }

        public static IAddress CreateAddress(HardwareType hardwareType)
        {
            var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.GetInterfaces().Contains(typeof(IAddress)) && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance((Type) t) as IAddress;

            return instances.FirstOrDefault(instance => instance.HardwareType == hardwareType);
        }


    }
}