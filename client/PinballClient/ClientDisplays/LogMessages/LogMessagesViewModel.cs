using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinballClient.Events;

namespace PinballClient.ClientDisplays.LogMessages
{
    [Export(typeof(ILogMessages))]
    public class LogMessagesViewModel : Screen, IHandle<LogEvent>, ILogMessages
    {
        private BindableCollection<LogEvent> _events;
        private IEventAggregator _eventAggregator;

        public string ClientDisplayName => "Log";

        public BindableCollection<LogEvent> Events
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

        [ImportingConstructor]
        public LogMessagesViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _events = new BindableCollection<LogEvent>();

            DisplayName = "Log";
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _eventAggregator.Subscribe(this);
            _eventAggregator.PublishOnUIThread(new DisplayLoadedEvent());
        }


        public void Handle(LogEvent message)
        {
            while(Events.Count >= 200)
            {
                // Remove from bottom if overflowing
                Events.RemoveAt(Events.Count - 1);
            }

            // Place most recent message at the top
            Events.Insert(0, message);
        }
    }
}
