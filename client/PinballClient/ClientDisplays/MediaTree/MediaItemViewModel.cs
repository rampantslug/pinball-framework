using System.Net.Mime;
using BusinessObjects;
using Caliburn.Micro;
using PinballClient.Events;

namespace PinballClient.ClientDisplays.MediaTree
{
    public class MediaItemViewModel : Screen
    {

        #region Fields

        private MediaType _mediaType;
        private IEventAggregator _eventAggregator;
        private string _name;

        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="name"></param>
        /// <param name="mediaType"></param>
        public MediaItemViewModel(IEventAggregator eventAggregator, string name, MediaType mediaType)
        {
            _eventAggregator = eventAggregator;
            _name = name;
            _mediaType = mediaType;
        }

        #endregion


        public void PlayMedia()
        {

            _eventAggregator.PublishOnUIThread(new ShowMediaEvent
            {
                MediaName = Name,
                MediaType = _mediaType
            });
        }

        public void Edit()
        {
        }

        public void Usage()
        {

        }

    }
}