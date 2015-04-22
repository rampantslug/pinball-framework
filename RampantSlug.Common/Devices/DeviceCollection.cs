using System;
using System.Collections.Generic;
using System.Linq;
using RampantSlug.Common.Logging;

namespace RampantSlug.Common.Devices
{
    public class DeviceCollection<V> : AttrCollection<ushort, string, IDevice>
    {
        public static AttrCollection<ushort, string, V> CreateCollection(List<V> devices, IRsLogManager logManager)
        {
            var collection = new AttrCollection<ushort, string, V>();
            foreach (var device in devices)
            {
                var iDevice = device as IDevice;
                if (iDevice != null)
                {
                    try
                    {
                        var address = ParseAddressString(iDevice.Address);
                        if (address == -1)
                        {
                            throw new Exception("Invalid device Address: " + iDevice.Address);
                        }
                        else
                        {
                            iDevice.Number = (ushort) address;
                            collection.Add(iDevice.Number, iDevice.Name, device);
                        }

                    }

                    catch
                        (Exception ex)
                    {
                        if (logManager != null)
                        {
                            logManager.LogTestMessage("Error with Device in config: " + iDevice.Name + " " + ex.Message);
                        }
                    }
                }
            }

            return collection;
        }


        private static int parse_matrix_num(string num)
        {
            string[] cr_list = num.Split('/');
            return (32 + Int32.Parse(cr_list[0]) * 16 + Int32.Parse(cr_list[1]));
        }

        private static int ParseAddressString(string address)
        {
            // Split out address into Controller and port
            var controllerPlusPort = address.Split('-');

            if (controllerPlusPort.Count() != 2)
            {
                // Invalid Address
                // Throw exception, error message and dont process the rest of this Device
                return -1;
            }

            switch (controllerPlusPort[0])
            {
                // Proc Switch Matrix Address
                case "PSM":
                {
                    return parse_matrix_num(controllerPlusPort[1]);
                }

                // Proc Direct Switch Address
                case "PDS":
                {
                    return Int32.Parse(controllerPlusPort[1]);
                }

                // Proc Driver Board (Coil?) Address
                case "PDB":
                {
                    return Int32.Parse(controllerPlusPort[1]);
                }

                // Proc Led Board Address
                case "PLB":
                {
                    return Int32.Parse(controllerPlusPort[1]);
                }

                // Arduino Servo Shield Address
                case "ASS":
                {
                    return Int32.Parse(controllerPlusPort[1]);
                }

                // Arduino Motor Shield Address
                case "AMS":
                {
                    return Int32.Parse(controllerPlusPort[1]);
                }
                default:
                {
                    return -1;
                }
            }

        }
    }
}