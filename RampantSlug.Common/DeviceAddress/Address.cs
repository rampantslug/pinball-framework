using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.DeviceAddress
{
    public abstract class Address 
    {
        public abstract string AddressString { get; protected set; }

        public abstract ushort AddressId { get; protected set; }

        public abstract string HardwareAcronym { get; }
        
        public abstract string HardwareDescription{ get; }



        public string GetHardwarePart(string addressString)
        {
            if (string.IsNullOrEmpty(addressString))
            {
                return string.Empty;
            }

            // Split out address into Controller and port
            var controllerPlusPort = addressString.Split('-');

            if (controllerPlusPort.Count() == 2)
            {
                return controllerPlusPort[0];
            }
            return string.Empty;
        }

        public string GetAddressPart(string addressString)
        {
            if (string.IsNullOrEmpty(addressString))
            {
                return string.Empty;
            }

            // Split out address into Controller and port
            var controllerPlusPort = addressString.Split('-');

            if (controllerPlusPort.Count() == 2)
            {
                return controllerPlusPort[1];
            }
            return string.Empty;
        }

        public bool HardwareMatchesThisAddress(string addressString)
        {
            return string.Equals(GetHardwarePart(addressString), HardwareAcronym);
        }

        public virtual void UpdateAddressString(string newAddress)
        {
            AddressString = newAddress;
            var address = GetAddressPart(AddressString);
            if (!string.IsNullOrEmpty(address))
            {
                ushort id;
                ushort.TryParse(address, out id);
                AddressId = id;
            }
        }

        public virtual void UpdateAddressId(ushort newId)
        {
            AddressId = newId;
            AddressString = HardwareAcronym + "-" + AddressId;
        }

    }
}
