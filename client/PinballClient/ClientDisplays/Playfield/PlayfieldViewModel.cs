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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common;
using PinballClient.CommonViewModels.Devices;
using PinballClient.Events;
using Path = System.Windows.Shapes.Path;

namespace PinballClient.ClientDisplays.Playfield
{
    [Export(typeof(IPlayfield))]
    public sealed class PlayfieldViewModel : Screen, IPlayfield,
        IHandle<UpdatePlayfieldImageEvent>,
        IHandle<CommonViewModelsLoadedEvent>,
        IHandle<AllSwitchesVisibilityEvent>,
        IHandle<AllCoilsVisibilityEvent>,
        IHandle<AllStepperMotorsVisibilityEvent>,
        IHandle<AllServosVisibilityEvent>,
        IHandle<AllLedsVisibilityEvent>,
        IHandle<HighlightSwitchEvent>
    {

        #region Fields

        private IEventAggregator _eventAggregator;
        private IGameState _gameState;

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

        private ObservableCollection<SwitchViewModel> _switches;
        private ObservableCollection<CoilViewModel> _coils;
        private ObservableCollection<StepperMotorViewModel> _stepperMotors;
        private ObservableCollection<ServoViewModel> _servos;
        private ObservableCollection<LedViewModel> _leds;
        private double _playfieldToLedsScale;

        #endregion

        #region Properties

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

        public double PlayfieldToLedsScale
        {
            get { return _playfieldToLedsScale; }
            set
            {
                _playfieldToLedsScale = value;
                NotifyOfPropertyChange(() => PlayfieldToLedsScale);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        [ImportingConstructor]
        public PlayfieldViewModel(IEventAggregator eventAggregator, IGameState gameState)
        {
            _eventAggregator = eventAggregator;
            _gameState = gameState;

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

            _playfieldToLedsScale = 0.25;
        }

        #endregion

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _eventAggregator.Subscribe(this);
            _eventAggregator.PublishOnUIThread(new DisplayLoadedEvent());
        }

        public void Handle(HighlightSwitchEvent message)
        {
            // TODO: Need to add devices to Playfield and then put highlight around selected object
            // message.Device...
        }

        /// <summary>
        /// Update playfield image based on received settings
        /// </summary>
        /// <param name="message"></param>
        public void Handle(UpdatePlayfieldImageEvent message)
        {
            PlayfieldImage = ImageConversion.ConvertStringToImage(message.PlayfieldImage);

        }

        /// <summary>
        /// Update tree based on received settings
        /// </summary>
        /// <param name="message"></param>
        public void Handle(CommonViewModelsLoadedEvent message)
        {
            Switches = _gameState.Switches;
            Coils = _gameState.Coils;
            StepperMotors = _gameState.StepperMotors;
            Servos = _gameState.Servos;
            Leds = _gameState.Leds;
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

        #region Handle Mouse movement / Dragging of Device

        public void MouseEnter(object source)
        {
//            var ledGeom = source as Path;
//            if (ledGeom != null)
//            {
//                var activeLed = ledGeom.DataContext as LedViewModel;
//                if (activeLed != null)
//                {
//                    activeLed.IsMouseOver = true;
//                }
//            }
        }

        public void MouseLeave(object source)
        {
            var ledGeom = source as Path;
            if (ledGeom != null)
            {
                var activeLed = ledGeom.DataContext as LedViewModel;
                if (activeLed != null)
                {
                    //activeLed.IsMouseOver = false;
                }
            }
        }

        public Point StartingPoint { get; set; }

        public void MouseDown(object source)
        {
            var ledGeom = source as Path;
            if (ledGeom != null)
            {
                StartingPoint = Mouse.GetPosition(Application.Current.MainWindow);

                // Find the Led we are moving and set it to selected device
                var activeLed = ledGeom.DataContext as LedViewModel;
                if (activeLed != null)
                {
                   // SelectedLed = activeLed;
                  //  SelectedLed.IsSelected = true;
                }
            }
        }

        public void MouseUp(object source)
        {
//            if (SelectedLed != null && !SelectedLed.IsMouseOver)
//            {
//                // De-Select selected Led
//                SelectedLed.IsSelected = false;
//                SelectedLed = null;
//            }
        }

        public void MouseMove(object source)
        {
//            if (Mouse.LeftButton == MouseButtonState.Pressed && SelectedLed != null)
//            {
//                var currentPoint = Mouse.GetPosition(Application.Current.MainWindow);
//                var xDelta = currentPoint.X - StartingPoint.X;
//                var yDelta = currentPoint.Y - StartingPoint.Y;
//
//                SelectedLed.LocationX = SelectedLed.LocationX + (double)(xDelta / ScaleFactorX);
//                SelectedLed.LocationY = SelectedLed.LocationY + (double)(yDelta / ScaleFactorY);
//
//                // Reset the starting point
//                StartingPoint = currentPoint;
//            }
        }

        #endregion
    }
}
