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
        IHandle<ConfigResults>,
        IHandle<HighlightDevice>
    {
        private ImageSource _playfieldImage;
        private IEventAggregator _eventAggregator;

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

        public PlayfieldViewModel()
        {
            DisplayName = "Playfield";
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);
        }

        public void Handle(ConfigResults message)
        {
            //DeserializeImage(message.MachineConfiguration.PlayfieldImage);
            //TestImage();
            PlayfieldImage = ImageConversion.ConvertStringToImage(message.MachineConfiguration.PlayfieldImage);
        }

        public void Handle(HighlightDevice message)
        {
            // TODO: Need to add devices to Playfield and then put highlight around selected object
            // message.Device...
        }


        public void DeserializeImage(byte[] encodedImage)
        {
            MemoryStream stream = new MemoryStream(encodedImage);
            PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            ImageSource iconSource = decoder.Frames[0];
            //PlayfieldImage = new Image();
            //PlayfieldImage.Width = 200;
            PlayfieldImage = iconSource;

            
        }

        public void TestImage()
        {
            //PlayfieldImage = new Image();
            //PlayfieldImage.Width = 200;

            //BitmapImage logo = new BitmapImage();
//logo.BeginInit();
            //logo.UriSource = new Uri("playfield.png", UriKind.Relative);
//logo.EndInit();

//PlayfieldImage = logo;
            var blobData = ImageConversion.ConvertImageFileToString("playfield.png");

            PlayfieldImage = ImageConversion.ConvertStringToImage(blobData);
        }
    }
}
