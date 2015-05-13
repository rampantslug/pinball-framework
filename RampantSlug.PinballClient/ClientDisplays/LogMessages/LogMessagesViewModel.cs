using Caliburn.Micro;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ClientDisplays.LogMessages
{
    //[Export(typeof(IClientDisplay))]
    public class LogMessagesViewModel : Screen, IHandle<LogEvent>, ILogMessages//, IClientDisplay
    {
        private BindableCollection<LogEvent> _events;

        public string ClientDisplayName { get { return "Log"; } }

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

        public LogMessagesViewModel() 
        {
           

            _events = new BindableCollection<LogEvent>();
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.Subscribe(this);
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
