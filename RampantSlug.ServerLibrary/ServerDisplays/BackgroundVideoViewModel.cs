using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RampantSlug.ServerLibrary.ServerDisplays
{
    [Export(typeof(IDisplayBackgroundVideo))]
    public sealed class BackgroundVideoViewModel : Screen, IDisplayBackgroundVideo
    {
        private MediaElement _backgroundVideoPlayer = new MediaElement();

        private Uri _backgroundVideoSource;
        private MediaPlayerState _backgroundVideoState;

        public MediaElement BackgroundVideoPlayer
        {
            get
            {
                return _backgroundVideoPlayer;
            }
            set
            {
                _backgroundVideoPlayer = value;
                NotifyOfPropertyChange(() => BackgroundVideoPlayer);
            }
        }

        public Uri BackgroundVideoSource
        {
            get
            {
                return _backgroundVideoSource;
            }
            set
            {
                _backgroundVideoSource = value;
                NotifyOfPropertyChange(() => BackgroundVideoSource);
            }
        }

        public MediaPlayerState BackgroundVideoState
        {
            get
            {
                return _backgroundVideoState;
            }
            set
            {
                _backgroundVideoState = value;
                NotifyOfPropertyChange(() => BackgroundVideoState);
            }
        }

        public BackgroundVideoViewModel()
        {
            BackgroundVideoPlayer.UnloadedBehavior = MediaState.Manual;
        }
    }
}
