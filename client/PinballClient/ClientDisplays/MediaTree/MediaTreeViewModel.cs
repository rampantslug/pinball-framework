using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BusinessObjects;
using PinballClient.Events;


namespace PinballClient.ClientDisplays.MediaTree
{
    [Export(typeof(IMediaTree))]
    public class MediaTreeViewModel : Screen, IMediaTree,
        IHandle<UpdateMediaEvent>
    {

        #region Fields

        private ObservableCollection<MediaItemViewModel> _mediaImages;
        private ObservableCollection<MediaItemViewModel> _mediaVideos;
        private ObservableCollection<MediaItemViewModel> _mediaSounds;

        private IEventAggregator _eventAggregator;
        private IGameState _gameState;

        #endregion

        #region Properties
        
        public ObservableCollection<MediaItemViewModel> MediaImages
        {
            get
            {
                return _mediaImages;
            }
            set
            {
                _mediaImages = value;
                NotifyOfPropertyChange(() => MediaImages);
            }
        }

        public ObservableCollection<MediaItemViewModel> MediaVideos
        {
            get
            {
                return _mediaVideos;
            }
            set
            {
                _mediaVideos = value;
                NotifyOfPropertyChange(() => MediaVideos);
            }
        }

        public ObservableCollection<MediaItemViewModel> MediaSounds
        {
            get
            {
                return _mediaSounds;
            }
            set
            {
                _mediaSounds = value;
                NotifyOfPropertyChange(() => MediaSounds);
            }
        }

        #endregion

        #region Constructor

        [ImportingConstructor]
        public MediaTreeViewModel(IEventAggregator eventAggregator, IGameState gameState)
        {
            _eventAggregator = eventAggregator;
            _gameState = gameState;
            DisplayName = "Media";
        }

        #endregion

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _eventAggregator.Subscribe(this);

            BuildTree();           
        }

        /// <summary>
        /// Update tree based on received settings
        /// </summary>
        /// <param name="message"></param>
        public void Handle(UpdateMediaEvent message)
        {
            BuildTree();
        }

        private void BuildTree() 
        {
            _mediaImages = new ObservableCollection<MediaItemViewModel>();
            foreach (var image in _gameState.Images)
            {
                _mediaImages.Add(new MediaItemViewModel(_eventAggregator, image, MediaType.Image));
            }

            _mediaVideos = new ObservableCollection<MediaItemViewModel>();
            foreach (var video in _gameState.Videos)
            {
                _mediaVideos.Add(new MediaItemViewModel(_eventAggregator, video, MediaType.Video));
            }

            _mediaSounds = new ObservableCollection<MediaItemViewModel>();
            foreach (var sound in _gameState.Sounds)
            {
                _mediaSounds.Add(new MediaItemViewModel(_eventAggregator, sound, MediaType.Sound));
            }

            NotifyOfPropertyChange(() => MediaImages);
            NotifyOfPropertyChange(() => MediaVideos);
            NotifyOfPropertyChange(() => MediaSounds);         
        }

    }
}
