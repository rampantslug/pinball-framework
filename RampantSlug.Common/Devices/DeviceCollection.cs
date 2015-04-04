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
                        // Create a unique Id / Number for the device
                        iDevice.UpdateNumberFromAddress();

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
    }
}