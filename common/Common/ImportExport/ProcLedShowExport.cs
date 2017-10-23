using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Configuration;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;


namespace Common.ImportExport
{
    /// <summary>
    /// Export a LedShow to the Proc LedShow format used by a PDLed board for 'advanced' RGB led control
    /// Documentation on the required formatting can be found here:
    /// http://www.pinballcontrollers.com/forum/index.php?topic=1116.15
    /// http://www.pinballcontrollers.com/forum/index.php?topic=1162.0
    /// Needs to save to .yaml
    ///  
    /// Example:
    /// - tocks: 5
    ///  LEDs:
    ///    CacodemonMouth: 00-f5
    ///    CacodemonEye: 0000-f5
    ///    Apron: ff0000-f5
    ///    LLane: 000000-f5
    ///    RLane: 000000-f5
    ///    LSling: 000000-f5
    ///    RSling: 000000-f5
    ///    LMid: 000000-f5
    ///    RMid: 000000-f5
    ///    BumperLoopGI: 000000-f5
    ///    BumperGI: 000000-f5
    ///    BackLeftGI: 000000-f5
    ///    BackRightGI: 000000-f5
    ///- tocks: 5
    ///  LEDs:
    ///    Apron: ff0000-f5
    ///    LLane: ff0000-f5
    ///    RLane: ff0000-f5
    ///    LSling: 000000-f5
    ///    RSling: 000000-f5
    ///    LMid: 000000-f5
    ///    RMid: 000000-f5
    ///    BumperLoopGI: 000000-f5
    ///    BumperGI: 000000-f5
    ///    BackLeftGI: 000000-f5
    ///    BackRightGI: 000000-f5
    ///- tocks: 5
    ///  LEDs:
    ///    Apron: ff0000-f5
    ///    LLane: ff0000-f5
    ///    RLane: ff0000-f5
    ///    LSling: ff0000-f5
    ///    RSling: ff0000-f5
    ///    LMid: 000000-f5
    ///    RMid: 000000-f5
    ///    BumperLoopGI: 000000-f5
    ///    BumperGI: 000000-f5
    ///    BackLeftGI: 000000-f5
    ///    BackRightGI: 000000-f5
    /// 
    /// 
    /// </summary>
    public class ProcLedShowExport : IShowExport
    {
        public ILedShowConfiguration Show { get; set; }

        private readonly StringBuilder _stringBuilder;

        public ProcLedShowExport(ILedShowConfiguration show)
        {
            Show = show;
            _stringBuilder = new StringBuilder();
        }

        public string GenerateTextString()
        {
            // Need to convert LedShow format into FlattenedEvents and then order them
            var flattenedEvents = CreateProcLedShowStructure();


            // Because of the funky format we cant use standard Yaml serialization from objects so have to do it manually :(

            const string initialContent = "---\nversion: 1\n";

            var sr = new StringReader(initialContent);
            var stream = new YamlStream();
            stream.Load(sr);

            var rootMappingNode = (YamlMappingNode)stream.Documents[0].RootNode;

            var topLevel = new YamlSequenceNode();

            foreach (var flattenedEvent in flattenedEvents)
            {
                // Need to create a Mapping Node
                var tocksNode = new YamlMappingNode
                {
                    {"tocks", flattenedEvent.tock.ToString()}
                };
                var ledsMapNode = new YamlMappingNode();
                foreach (var simpleLed in flattenedEvent.Leds)
                {
                    // ToString returns #ff0576aa format. Need 0576aa. So... 
                    // Remove # and the alpha value
                    var ledColor = simpleLed.LedColor.ToString().Remove(0,3); 
                    var ledValue = ledColor + "-f" + simpleLed.FadeTocks;
                    ledsMapNode.Add(simpleLed.LedName, ledValue);
                }

                tocksNode.Add("LEDS", ledsMapNode);
                topLevel.Add(tocksNode);
            }

            rootMappingNode.Add("", topLevel);


            //            var props = new YamlMappingNode
            //            {
            //                {"prop1", "value1"},
            //                { "prop2", "value2"}
            //            };
            //            rootMappingNode.Add("itemWithProps", props);

            var sw = new StringWriter();
            stream.Save(sw, false);

            using (TextWriter writer = File.CreateText("C:\\temp\\test.yaml"))
                stream.Save(writer, false);









           // var serializer = new Serializer();

            //var sw= new StringWriter();

           // var writer = File.CreateText("test.yml");
            //serializer.Serialize(sw, flattenedEvents);
            //writer.Close();

            return sw.ToString();
        }

        public List<FlattenedEvent> CreateProcLedShowStructure()
        {
            var flattenedEvents = new List<FlattenedEvent>();
            var startEndTimes = new List<uint>();
            var sortedEventTimes = new List<uint>();

            foreach (var ledInShow in Show.Leds)
            {
                foreach (var ledEvent in ledInShow.Events)
                {
                    // Get all the start and end times to generate a complete list of events THEN add the leds to them
                    startEndTimes.Add(ledEvent.StartFrame);
                    startEndTimes.Add(ledEvent.EndFrame);

                    startEndTimes.Sort();
                    sortedEventTimes = startEndTimes.Distinct().ToList();
                }
            }

            for (var i = 0; i < sortedEventTimes.Count - 1; i++)
            {
                var flattenedEvent = new FlattenedEvent()
                {
                    StartTock = sortedEventTimes[i],
                    EndTock = sortedEventTimes[i + 1],
                };

                // Check each Led for events that fit in the timeslice
                foreach (var ledInShow in Show.Leds)
                {
                    foreach (var ledEvent in ledInShow.Events)
                    {
                        if (DoesOverlap(flattenedEvent.StartTock, flattenedEvent.EndTock, ledEvent.StartFrame,
                            ledEvent.EndFrame))
                        {
                            // Led needs to exist in this timeslice
                            var simpleLed = new SimpleLed()
                            {
                                LedName = ledInShow.LinkedLedName,
                                LedColor = ledEvent.EndColor,
                                FadeTocks = ledEvent.EndFrame - ledEvent.StartFrame
                                // NOTE: Not sure how this works at the moment?
                            };
                            flattenedEvent.Leds.Add(simpleLed);

                            // Move onto next led
                            break;
                        }
                    }
                }
                flattenedEvents.Add(flattenedEvent);
            }
            return flattenedEvents;
        }



        private bool DoesOverlap(uint expectedStartTime, uint expectedEndTime, uint comparingStartTime,
            uint comparingEndTime)
        {
            if (comparingStartTime >= expectedStartTime && comparingStartTime < expectedEndTime)
            {
                return true;
            }

            if (comparingEndTime > expectedStartTime && comparingEndTime <= expectedEndTime)
            {
                return true;
            }
            return false;
        }
    }

    public class FlattenedEvent
    {
        [YamlIgnore]
        public uint StartTock { get; set; }

        [YamlIgnore]
        public uint EndTock { get; set; }

        // ReSharper disable once InconsistentNaming - To match format
        public uint tock => EndTock - StartTock;

        public List<SimpleLed> Leds { get; set; }

        public FlattenedEvent()
        {
            Leds = new List<SimpleLed>();
        }
    }

    public class SimpleLed
    {
        public string LedName { get; set; }

        public Color LedColor { get; set; }

        public uint FadeTocks { get; set; }
    }
}