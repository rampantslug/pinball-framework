using System.Windows.Media;

namespace BusinessObjects.LedShows
{
    public class LedEvent
    {
        public uint StartFrame { get; set; }

        public uint EndFrame { get; set; }

        public Color StartColor { get; set; }

        public Color EndColor { get; set; }
    }
}