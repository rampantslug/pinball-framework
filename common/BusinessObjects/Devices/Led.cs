using System.Windows.Media;
using BusinessObjects.Shapes;

namespace BusinessObjects.Devices
{
    /// <summary>
    /// Represents a led device in a pinball machine.
    /// </summary>
    public class Led : Device, IDevice
    {
        public LedShape Shape { get; set; }

        public bool IsSingleColor { get; set; }

        public Color SingleColor { get; set; }

        public Led()
        {

        }

        public override string StateString => IsActive() ? "on" : "off";

        public void MidIntensityOn()
        {

        }
    }
}
