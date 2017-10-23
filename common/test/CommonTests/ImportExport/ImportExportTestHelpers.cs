using System.Windows.Media;
using BusinessObjects.LedShows;
using Configuration;

namespace CommonTests.ImportExport
{
    public class ImportExportTestHelpers
    {
        public static ILedShowConfiguration CreateTestLedShowConfiguration()
        {
            var ledShow = new LedShowConfiguration()
            {
                Frames = 16,
                Name = "TestShow",
            };
            ledShow.Leds.Add(CreateLedInShow());
            return ledShow;
        }

        public static LedInShow CreateLedInShow()
        {
            var ledInShow = new LedInShow()
            {
                LinkedLedId = 1,
                LinkedLedName = "TestLed"
            };
            var event1 = new LedEvent()
            {
                StartFrame = 1,
                EndFrame = 4,
                StartColor = Colors.Blue,
                EndColor = Colors.Blue
            };
            var event2 = new LedEvent()
            {
                StartFrame = 4,
                EndFrame = 8,
                StartColor = Colors.Blue,
                EndColor = Colors.Blue
            };
            var event3 = new LedEvent()
            {
                StartFrame = 12,
                EndFrame = 16,
                StartColor = Colors.Blue,
                EndColor = Colors.Blue
            };
            ledInShow.Events.Add(event1);
            ledInShow.Events.Add(event2);
            ledInShow.Events.Add(event3);
            return ledInShow;
        }

    }
}
