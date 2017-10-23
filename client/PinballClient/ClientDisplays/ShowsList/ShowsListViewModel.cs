using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Common.ImportExport;
using PinballClient.CommonViewModels;


namespace PinballClient.ClientDisplays.ShowsList
{
    [Export(typeof(IShowsList))]
    class ShowsListViewModel : Screen, IShowsList
    {
        private readonly IEventAggregator _eventAggregator;
        public IGameState GameState { get; set; }

        [ImportingConstructor]
        public ShowsListViewModel(IEventAggregator eventAggregator, IGameState gameState)
        {
            _eventAggregator = eventAggregator;
            DisplayName = "Shows";
            GameState = gameState;
        }


        public void ImportExistingShow()
        {
            // TODO: open file browser window
        }

        public void ExportLampShow(LedShowViewModel show)
        {
            var lampShowExport = new ProcLampShowExport(show.ConvertToConfiguration());
            var output = lampShowExport.GenerateTextString();

            var filePath = @"LampShows\" + show.Name + ".lampshow";
            File.WriteAllText(filePath, output);
        }

        public void ExportLedShow(LedShowViewModel show)
        {         
            var ledShowExport = new ProcLedShowExport(show.ConvertToConfiguration());

            var output = ledShowExport.GenerateTextString();

            var filePath = @"LedShows\" + show.Name + ".yml";
            File.WriteAllText(filePath, output);
        }

        public void AddShow()
        {
            GameState.Shows.Add(new LedShowViewModel(_eventAggregator));
        }


        public void DeleteShow(LedShowViewModel show)
        {
            if (show != null)
            {
                GameState.Shows.Remove(show);
                // Also need to remove physical file
                //TODO: Fix below...
//                var additionalpath = WorkingDirectory + @"LedShows\" + show.Name + ".json";
//                if (File.Exists(additionalpath))
//                {
//                    File.Delete(additionalpath);
//                }
            }
        }

        public void DuplicateShow(LedShowViewModel show)
        {
//            if (show != null)
//            {
//                AddShow();
//                var duplicate = GameState.Shows.Last();
//
//                duplicate.Frames = show.Frames;
//                // Need to perform a deep copy for leds
//                foreach (var led in show.Leds)
//                {
//                    var duplicateLed = new LedInShowViewModel(_eventAggregator, led.LinkedLed);
//                    foreach (var eventVm in led.Events)
//                    {
//                        var duplicateEvent = new EventViewModel(eventVm.StartFrame, eventVm.EndFrame, eventVm.StartColor, eventVm.EndColor);
//                        duplicateLed.Events.Add(duplicateEvent);
//                    }
//                    duplicate.Leds.Add(duplicateLed);
//                }
//            }
        }

    }
}
