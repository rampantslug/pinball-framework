using System.Collections.Generic;

namespace BusinessObjects.LedShows
{
    public class LedInShow
    {
        public uint LinkedLedId { get; set; }

        public string LinkedLedName { get; set; }
        public IList<LedEvent> Events { get; set; }

        public LedInShow()
        {
            Events = new List<LedEvent>();
        }
    }
}