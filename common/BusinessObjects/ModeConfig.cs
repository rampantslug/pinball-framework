using System.Collections.Generic;

namespace BusinessObjects
{
    public class Mode
    {
        public string Title { get; set; }
        public List<ModeEventConfig> ModeEvents { get; set; }
        public List<RequiredDeviceConfig> RequiredDevices { get; set; }
        public List<RequiredMediaConfig> RequiredMedia { get; set; }
    }
}