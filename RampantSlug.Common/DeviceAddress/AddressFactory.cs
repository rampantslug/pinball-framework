using System;
using System.Linq;
using System.Reflection;

namespace RampantSlug.Common.DeviceAddress
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
        }


        public static IAddress CreateAddress(string addressString)
        {
            // Split out address into Controller and port
            var controllerPlusPort = addressString.Split('-');
            if (controllerPlusPort.Count() != 2)
            {
                return null;
            }

            var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.GetInterfaces().Contains(typeof(IAddress)) && t.GetConstructor(Type.EmptyTypes) != null
                select Activator.CreateInstance(t) as IAddress;

            foreach (var instance in instances)
            {
                if (instance.HardwareMatchesThisAddress(addressString))
                {
                    instance.UpdateAddressString(addressString);
                    return instance;
                }
            }

            return null;           
        }

        public static IAddress CreateAddress(HardwareType hardwareType)
        {
            var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.GetInterfaces().Contains(typeof(IAddress)) && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as IAddress;

            return instances.FirstOrDefault(instance => instance.HardwareType == hardwareType);
        }


    }
}