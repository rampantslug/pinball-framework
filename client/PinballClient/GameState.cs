using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Caliburn.Micro;
using Common;
using PinballClient.CommonViewModels;
using PinballClient.CommonViewModels.Devices;

namespace PinballClient
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IGameState))]
    public class GameState: Screen, IGameState
    {
        #region Fields

        private ObservableCollection<SwitchViewModel> _switches;
        private ObservableCollection<CoilViewModel> _coils;
        private ObservableCollection<StepperMotorViewModel> _stepperMotors;
        private ObservableCollection<ServoViewModel> _servos;
        private ObservableCollection<LedViewModel> _leds;

        private string _playfieldImage;
        private IObservableCollection<LedShowViewModel> _shows;

        #endregion

        #region Properties

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

        public string PlayfieldImage
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

        public IObservableCollection<LedShowViewModel> Shows
        {
            get
            {
                return _shows;
            }
            set
            {
                _shows = value;
                NotifyOfPropertyChange(() => Shows);
            }
        }


        public List<Mode> Modes { get; set; }

        public List<string> Images { get; set; }
        public List<string> Videos { get; set; }
        public List<string> Sounds { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public GameState()
        {
            Switches = new ObservableCollection<SwitchViewModel>();
            Coils = new ObservableCollection<CoilViewModel>();
            StepperMotors = new ObservableCollection<StepperMotorViewModel>();
            Servos = new ObservableCollection<ServoViewModel>();
            Leds = new ObservableCollection<LedViewModel>();

            Modes = new List<Mode>();

            Shows = new BindableCollection<LedShowViewModel>();

            Images = new List<string>();
            Videos = new List<string>();
            Sounds = new List<string>();
        }

        #endregion
    }
}
