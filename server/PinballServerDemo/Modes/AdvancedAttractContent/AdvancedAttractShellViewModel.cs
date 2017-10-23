using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using ServerLibrary.ServerDisplays;


namespace PinballServerDemo.Modes.AdvancedAttractContent
{
    [Export(typeof(AdvancedAttractShellViewModel))]
    public sealed class AdvancedAttractShellViewModel : Screen
    {
        private MediaElement _backgroundVideoPlayer = new MediaElement();
        private Uri _backgroundVideoSource;
        private MediaPlayerState _backgroundVideoState;
        private AdvancedAttract _parent;
        private string _mainText;


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

        public string MainText

        {
            get
            {
                return _mainText;
            }
            set
            {
                _mainText = value;
                NotifyOfPropertyChange(() => MainText);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="advancedAttract"></param>
        public AdvancedAttractShellViewModel(AdvancedAttract advancedAttract)
        {
            _parent = advancedAttract;
            BackgroundVideoPlayer.UnloadedBehavior = MediaState.Manual;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            if (!string.IsNullOrEmpty(_parent.IntroVideo.Filename))
            {
                BackgroundVideoSource = new Uri(_parent.IntroVideo.Filename);
                BackgroundVideoPlayer.Play();
                BackgroundVideoState = MediaPlayerState.Playing;
            }  
        }

        public void IntroFinished()
        {
            BackgroundVideoPlayer.Visibility = Visibility.Collapsed;
            BackgroundVideoPlayer.Source = null;

            MainText = "Press Start";
        }
    }
}
