using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BusinessObjects.LedShows;
using Caliburn.Micro;
using PinballClient.CommonViewModels.Devices;
using PinballClient.Events;

namespace PinballClient.CommonViewModels
{
    public class LedInShowViewModel : Screen, IHandle<SingleColorLedColorModifiedEvent>
    {
        public IObservableCollection<EventViewModel> Events
        {
            get
            {
                return _events;
            }
            set
            {
                _events = value;
                NotifyOfPropertyChange(() => Events);
            }
        }

        public LedViewModel LinkedLed { get; set; }

        public int ShiftAmount
        {
            get { return _shiftAmount; }
            set
            {
                _shiftAmount = value;
                ShiftAllEvents(_shiftAmount);
                _shiftAmount = 0;
                NotifyOfPropertyChange(() => ShiftAmount);

            }
        }


        public LedInShowViewModel(IEventAggregator eventAggregator, LedViewModel linkedLed)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            LinkedLed = linkedLed;
            _events = new BindableCollection<EventViewModel>();
        }


        public void DeleteEvent(EventViewModel dataContext)
        {
            Events.Remove(dataContext);
        }

        public void ShiftAllEvents(int shiftAmount)
        {
            // TODO: Check to see if shift makes sense e.g not some masive amount or something that will make StartFrame neg.
            foreach (var eventViewModel in Events)
            {
                eventViewModel.StartFrame = (uint)(eventViewModel.StartFrame + shiftAmount);
                eventViewModel.EndFrame = (uint)(eventViewModel.EndFrame + shiftAmount);
            }
        }

        public void Handle(SingleColorLedColorModifiedEvent message)
        {
            if (message.Led == LinkedLed)
            {
                foreach (var eventViewModel in Events)
                {
                    // Dont modify transparent values as we want to keep fades
                    if (eventViewModel.StartColor != Colors.Transparent)
                    {
                        eventViewModel.StartColor = message.NewColor;
                    }
                    if (eventViewModel.EndColor != Colors.Transparent)
                    {
                        eventViewModel.EndColor = message.NewColor;
                    }
                }
            }
        }

        public EventViewModel GetLastEvent()
        {
            if (Events.Any())
            {
                var lastEvent = Events.First();
                foreach (var eventViewModel in Events)
                {
                    if (eventViewModel.EndFrame >= lastEvent.EndFrame)
                    {
                        lastEvent = eventViewModel;
                    }
                }
                return lastEvent;
            }
            return null;
        }

        public LedInShow ConvertToConfiguration()
        {
            var configuration = new LedInShow()
            {
                LinkedLedId = LinkedLed.Number,
                LinkedLedName = LinkedLed.Name
            };
            foreach (var ledEvent in Events)
            {
                var ledEventConfig = ledEvent.ConvertToConfiguration();
                configuration.Events.Add(ledEventConfig);
            }

            return configuration;
        }


        private IObservableCollection<EventViewModel> _events;
        private IEventAggregator _eventAggregator;
        private int _shiftAmount;

      
    }
}