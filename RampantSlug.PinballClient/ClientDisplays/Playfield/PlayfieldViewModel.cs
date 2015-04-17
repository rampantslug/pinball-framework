using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RampantSlug.PinballClient.Events;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RampantSlug.Common;
using RampantSlug.PinballClient.CommonViewModels;

namespace RampantSlug.PinballClient.ClientDisplays.Playfield
{
    [Export(typeof(PlayfieldViewModel))]
    public sealed class PlayfieldViewModel : Screen, IPlayfield, 
        IHandle<UpdatePlayfieldImage>,
        IHandle<CommonViewModelsLoaded>,
        IHandle<AllSwitchesVisibility>,
        IHandle<AllCoilsVisibility>,
        IHandle<AllStepperMotorsVisibility>,
        IHandle<AllServosVisibility>,
        IHandle<HighlightSwitch>
    {
        private IEventAggregator _eventAggregator;
        private ImageSource _playfieldImage;
        private double _playfieldWidth;
        private double _playfieldHeight;
        private bool _allSwitchesVis;
        private bool _allCoilsVis;
        private Visibility _allStepperMotorsVis;
        private Visibility _allServosVis;

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

        public bool AllSwitchesVis
        {
            get
            {
                return _allSwitchesVis;
            }
            private set
            {
                _allSwitchesVis = value;
                NotifyOfPropertyChange(() => AllSwitchesVis);
            } 
        }

        public bool AllCoilsVis
        {
            get
            {
                return _allCoilsVis;
            }
            private set
            {
                _allCoilsVis = value;
                NotifyOfPropertyChange(() => AllCoilsVis);
            }
        }

        public Visibility AllStepperMotorsVis
        {
            get
            {
                return _allStepperMotorsVis;
            }
            private set
            {
                _allStepperMotorsVis = value;
                NotifyOfPropertyChange(() => AllStepperMotorsVis);
            }
        }

        public Visibility AllServosVis
        {
            get
            {
                return _allServosVis;
            }
            private set
            {
                _allServosVis = value;
                NotifyOfPropertyChange(() => AllServosVis);
            }
        }

        private ObservableCollection<SwitchViewModel> _switches;
        public ObservableCollection<SwitchViewModel> Switches
        {
            get
            {
                return _switches;
            }
            set
            {
                _switches = value;
                NotifyOfPropertyChange(() => Switches);
            }
        }

        private ObservableCollection<CoilViewModel> _coils;
        public ObservableCollection<CoilViewModel> Coils
        {
            get
            {
                return _coils;
            }
            set
            {
                _coils = value;
                NotifyOfPropertyChange(() => Coils);
            }
        }


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public PlayfieldViewModel()
        {
            DisplayName = "Playfield";
            PlayfieldWidth = 400;
            PlayfieldHeight = 800;
            AllSwitchesVis = true;
            AllCoilsVis = true;
            AllStepperMotorsVis = Visibility.Visible;
            AllServosVis = Visibility.Visible;

            _switches = new ObservableCollection<SwitchViewModel>();
            _coils = new ObservableCollection<CoilViewModel>();
        }

        #endregion

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

        /// <summary>
        /// Update tree based on received settings
        /// </summary>
        /// <param name="message"></param>
        public void Handle(CommonViewModelsLoaded message)
        {
            var shellViewModel = IoC.Get<IShell>();

            Switches = shellViewModel.Switches;
            Coils = shellViewModel.Coils;

        }

        public void Handle(AllSwitchesVisibility message)
        {
            AllSwitchesVis = message.IsVisible;
        }

        public void Handle(AllCoilsVisibility message)
        {
            AllCoilsVis = message.IsVisible;
        }

        public void Handle(AllStepperMotorsVisibility message)
        {
            AllStepperMotorsVis = message.IsVisible ? Visibility.Visible : Visibility.Hidden;
        }

        public void Handle(AllServosVisibility message)
        {
            AllServosVis = message.IsVisible ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
