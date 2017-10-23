using Newtonsoft.Json;

namespace BusinessObjects
{
    public class RequiredDeviceConfig
    {
        [JsonIgnore]
        public ushort Id { get; set; }

        public string Name { get; set; }

        public string TypeOfDevice { get; set; }

        public string DeviceName { get; set; }
    }
}