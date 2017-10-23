using System.Collections.Generic;
using BusinessObjects.LedShows;

namespace Configuration
{
    public class LedShowConfiguration : ILedShowConfiguration
    {
        public string Name { get; set; }

        public uint Frames { get; set; }
        public IList<LedInShow> Leds { get; set; }

        public LedShowConfiguration()
        {
            Leds = new List<LedInShow>();
        }
    }
}
