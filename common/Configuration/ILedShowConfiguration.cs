using System.Collections.Generic;
using BusinessObjects.LedShows;

namespace Configuration
{
    public interface ILedShowConfiguration 
    {
        string Name { get; set; }
        uint Frames { get; set; }

        IList<LedInShow> Leds { get; set; }
    }
}
