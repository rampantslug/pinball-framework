using System.Text;
using System.Windows.Media;
using BusinessObjects.LedShows;
using Configuration;

namespace Common.ImportExport
{
    /// <summary>
    /// Export a LedShow to the Proc LampShow format used by a PD8x8 board for 'simple' lamp/led control.
    /// Documentation on the required formatting can be found here:
    /// http://pyprocgame.pindev.org/ref/lamps.html
    /// Needs to save to .lampshow
    /// 
    /// Example:
    /// ##############################################################################################################
    /// # Markers:                                  |        8      16      24      32      40      48      56      64
    /// # Frames:                                   | 1234567812345678123456781234567812345678123456781234567812345678
    /// lamp:Willie                                 |     ........                            ........        
    /// lamp:Sallah                                 |         ........                    ........            
    /// lamp:Marion                                 |             ........            ........                
    /// lamp:DrJones                                |                 ........    ........                    
    /// lamp:Shorty                                 | ........                                    ........  
    /// 
    /// </summary>
    public class ProcLampShowExport : IShowExport
    {
        private const int LineWidthToPipe = 44;
        private const int LineWidthToFirstFrame = 46;

        public ILedShowConfiguration Show { get; set; }

        private readonly StringBuilder _stringBuilder;

        public ProcLampShowExport(ILedShowConfiguration show)
        {
            Show = show;
            _stringBuilder = new StringBuilder();
        }

        public string GenerateTextString()
        {
            _stringBuilder.Clear();
            GenerateHeader();

            // Add a single line for each led
            foreach (var ledInShowViewModel in Show.Leds)
            {
                var ledLine = GetLedLine(ledInShowViewModel);
                _stringBuilder.AppendLine(ledLine);
            }
            return _stringBuilder.ToString();
        }

        private void GenerateHeader()
        {
            var hashLine = "";
            hashLine = hashLine.PadRight(LineWidthToFirstFrame + 64, '#');
            _stringBuilder.AppendLine(hashLine);
            _stringBuilder.AppendLine("# Lightshow: " + Show.Name);
            _stringBuilder.AppendLine("# Type: simple");
            _stringBuilder.AppendLine("# Length: " + Show.Frames + " frames - Approx " + Show.Frames / 32 + " seconds");
            _stringBuilder.AppendLine(@"# Created using RampantSlug Framework - Get it at https://bitbucket.org/rampantslug/pinball-framework");
            _stringBuilder.AppendLine(hashLine);

            var markers = "# Markers:";
            var frames = "# Frames:";
            markers = markers.PadRight(LineWidthToPipe);
            frames = frames.PadRight(LineWidthToPipe);
            markers = markers + "|        8      16      24      32      40      48      56      64";
            frames = frames + "| 1234567812345678123456781234567812345678123456781234567812345678";
            _stringBuilder.AppendLine(markers);
            _stringBuilder.AppendLine(frames);
        }

        public string GetLedLine(LedInShow ledInShowViewModel)
        {
            var ledSimpleName = ledInShowViewModel.LinkedLedName.Replace(" ", ""); // Remove spaces
            var line = "lamp:" + ledSimpleName;
            line = line.PadRight(LineWidthToPipe);
            line = line + "| ";

            char[] timelineRow = new char[Show.Frames];

            // Initialise each char to be a space
            for (var i = 0; i < Show.Frames; i++)
            {
                timelineRow[i] = ' ';
            }

            foreach (var eventVm in ledInShowViewModel.Events)
            {
                // Handle case where events have been placed outside the max frames of the show
                // Normally this shouldnt be a problem (More error handling may be require elsewhere)
                var endFrame = eventVm.EndFrame;
                if (eventVm.StartFrame >= Show.Frames)
                {
                    break;
                }
                if (endFrame > Show.Frames)
                {
                    endFrame = Show.Frames;
                }

                // No change so normal process
                if (eventVm.StartColor == eventVm.EndColor)
                {
                    for (var i = eventVm.StartFrame; i < endFrame; i++)
                    {
                        timelineRow[i] = '.';
                    }
                }
                // Fade In
                else if (eventVm.StartColor == Colors.Transparent)
                {
                    timelineRow[eventVm.StartFrame] = '<';
                    for (var i = eventVm.StartFrame + 1; i < endFrame - 1; i++)
                    {
                        timelineRow[i] = ' ';
                    }
                    timelineRow[endFrame - 1] = ']';
                }
                // Fade Out
                else if (eventVm.EndColor == Colors.Transparent)
                {
                    timelineRow[eventVm.StartFrame] = '[';
                    for (var i = eventVm.StartFrame + 1; i < endFrame - 1; i++)
                    {
                        timelineRow[i] = ' ';
                    }
                    timelineRow[endFrame - 1] = '>';
                }
            }
            var rowString = new string(timelineRow);
            line = line + rowString;
            return line;            
        }
    }
}
