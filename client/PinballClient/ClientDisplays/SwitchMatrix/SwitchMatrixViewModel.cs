using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hardware.DeviceAddress;
using PinballClient.Events;


namespace PinballClient.ClientDisplays.SwitchMatrix
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(ISwitchMatrix))]
    public sealed class SwitchMatrixViewModel : Screen, ISwitchMatrix, IHandle<CommonViewModelsLoadedEvent>
    {
        #region Fields

        private ObservableCollection<SwitchMatrixItemViewModel> _switches;
        private IEventAggregator _eventAggregator;
        private IGameState _gameState;

        #endregion

        #region Properties

        public ObservableCollection<SwitchMatrixItemViewModel> Switches
        {
            get { return _switches; }
            set
            {
                _switches = value;
                NotifyOfPropertyChange(() => Switches);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="gameState"></param>
        [ImportingConstructor]
        public SwitchMatrixViewModel(IEventAggregator eventAggregator, IGameState gameState)
        {
            _eventAggregator = eventAggregator;
            _gameState = gameState;

            DisplayName = "Switch Matrix";
        }

        #endregion

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _eventAggregator.Subscribe(this);

            RebuildMatrix();
        }

        /// <summary>
        /// Update Switch Matrix based on received settings
        /// </summary>
        /// <param name="message"></param>
        public void Handle(CommonViewModelsLoadedEvent message)
        {
            RebuildMatrix();
        }

        private void RebuildMatrix()
        {
            Switches = new ObservableCollection<SwitchMatrixItemViewModel>();

            foreach (var switchViewModel in _gameState.Switches)
            {
                if (switchViewModel.Address.HardwareType == AddressFactory.HardwareType.ProcSwitchMatrix)
                {
                    Switches.Add(new SwitchMatrixItemViewModel(switchViewModel));
                }
            }
            
        }
    }
}
