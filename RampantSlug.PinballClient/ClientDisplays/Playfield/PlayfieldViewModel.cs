using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.PinballClient.Events;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RampantSlug.Common;

namespace RampantSlug.PinballClient.ClientDisplays.Playfield
{
    [Export(typeof(PlayfieldViewModel))]
    public sealed class PlayfieldViewModel : Screen, IPlayfield, 
        IHandle<UpdatePlayfieldImage>,
        IHandle<HighlightSwitch>
    {
        private IEventAggregator _eventAggregator;
        private ImageSource _playfieldImage;
        private double _playfieldWidth;
        private double _playfieldHeight;

        public ImageSource PlayfieldImage
        {
            get
            {
                return _playfieldImage;
            }
            set
            {
                _playfieldImage = value;
                NotifyOfPropertyChange(() => PlayfieldImage);
            }
        }

        public double PlayfieldWidth
        {
            get
            {
                return _playfieldWidth;
            }
            set
            {
                _playfieldWidth = value;
                NotifyOfPropertyChange(() => PlayfieldWidth);
            }
        }

        public double PlayfieldHeight
        {
            get
            {
                return _playfieldHeight;
            }
            set
            {
                _playfieldHeight = value;
                NotifyOfPropertyChange(() => PlayfieldHeight);
            }
        }

        public PlayfieldViewModel()
        {
            DisplayName = "Playfield";
            PlayfieldWidth = 400;
            PlayfieldHeight = 800;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);

        }

        public void Handle(HighlightSwitch message)
        {
            // TODO: Need to add devices to Playfield and then put highlight around selected object
            // message.Device...
        }

        /// <summary>
        /// Update playfield image based on received settings
        /// </summary>
        /// <param name="message"></param>
        public void Handle(UpdatePlayfieldImage message)
        {
            PlayfieldImage = ImageConversion.ConvertStringToImage(message.PlayfieldImage);

        }

    }
}
