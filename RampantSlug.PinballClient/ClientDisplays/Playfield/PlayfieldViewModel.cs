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
        IHandle<AllSwitchesVisibilityEvent>,
        IHandle<AllCoilsVisibilityEvent>,
        IHandle<AllStepperMotorsVisibilityEvent>,
        IHandle<AllServosVisibilityEvent>,
        IHandle<AllLedsVisibilityEvent>,
        IHandle<HighlightSwitch>
    {
        private IEventAggregator _eventAggregator;
        private ImageSource _playfieldImage;
        private double _playfieldWidth;
        private double _playfieldHeight;
        private bool _allSwitchesVis;
        private bool _allCoilsVis;
        private bool _allStepperMotorsVis;
        private bool _allServosVis;
        private bool _allLedsVis;

        private double _scaleFactorX;
        private double _scaleFactorY;

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

                // Update ScaleFactor
                ScaleFactorX = PlayfieldWidth / 100;
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

                // Update ScaleFactor
                ScaleFactorY = PlayfieldHeight/100;

            }
        }

        public double ScaleFactorX
        {
            get
            {
                return _scaleFactorX;
            }
            set
            {
                _scaleFactorX = value;
                NotifyOfPropertyChange(() => ScaleFactorX);
            }
        }

        public double ScaleFactorY
        {
            get
            {
                return _scaleFactorY;
            }
            set
            {
                _scaleFactorY = value;
                NotifyOfPropertyChange(() => ScaleFactorY);
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

        public bool AllStepperMotorsVis
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

        public bool AllServosVis
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

        public bool AllLedsVis
        {
            get
            {
                return _allLedsVis;
            }
            private set
            {
                _allLedsVis = value;
                NotifyOfPropertyChange(() => AllLedsVis);
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

        private ObservableCollection<StepperMotorViewModel> _stepperMotors;
        public ObservableCollection<StepperMotorViewModel> StepperMotors
        {
            get
            {
                return _stepperMotors;
            }
            set
            {
                _stepperMotors = value;
                NotifyOfPropertyChange(() => StepperMotors);
            }
        }

        private ObservableCollection<ServoViewModel> _servos;
        public ObservableCollection<ServoViewModel> Servos
        {
            get
            {
                return _servos;
            }
            set
            {
                _servos = value;
                NotifyOfPropertyChange(() => Servos);
            }
        }

        private ObservableCollection<LedViewModel> _leds;


        public ObservableCollection<LedViewModel> Leds
        {
            get
            {
                return _leds;
            }
            set
            {
                _leds = value;
                NotifyOfPropertyChange(() => Leds);
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
            AllStepperMotorsVis = true;
            AllServosVis = true;
            AllLedsVis = true;

            _switches = new ObservableCollection<SwitchViewModel>();
            _coils = new ObservableCollection<CoilViewModel>();
            _stepperMotors = new ObservableCollection<StepperMotorViewModel>();
            _servos = new ObservableCollection<ServoViewModel>();
            _leds = new ObservableCollection<LedViewModel>();
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
            StepperMotors = shellViewModel.StepperMotors;
            Servos = shellViewModel.Servos;
            Leds = shellViewModel.Leds;

        }

        public void Handle(AllSwitchesVisibilityEvent message)
        {
            AllSwitchesVis = message.IsVisible;
        }

        public void Handle(AllCoilsVisibilityEvent message)
        {
            AllCoilsVis = message.IsVisible;
        }

        public void Handle(AllStepperMotorsVisibilityEvent message)
        {
            AllStepperMotorsVis = message.IsVisible;
        }

        public void Handle(AllServosVisibilityEvent message)
        {
            AllServosVis = message.IsVisible;
        }

        public void Handle(AllLedsVisibilityEvent message)
        {
            AllLedsVis = message.IsVisible;
        }
    }
}
