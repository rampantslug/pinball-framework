using System;
using System.Collections.Generic;
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
                        ushort number = 0;
                        ushort.TryParse(iDevice.Address, out number);
                        
                        // Create a unique Id / Number for the device
                        if (iDevice.Address.Contains("/"))
                        {
                            number = (ushort) parse_matrix_num(iDevice.Address);
                        }

                        iDevice.Number = number;



                        collection.Add(iDevice.Number, iDevice.Name, device);
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
    }
}