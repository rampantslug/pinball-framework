using Caliburn.Micro;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PinballClient.Events;

namespace PinballClient.ClientDisplays.ModeTree
{
    [Export(typeof(IModeTree))]
    public class ModeTreeViewModel: Screen, IModeTree,
        IHandle<UpdateModesEvent>
    {


        #region Properties

        public ObservableCollection<ModeItemViewModel> ModeItems => _modeItems;

        #endregion 
            
        #region Constructor

        [ImportingConstructor]
        public ModeTreeViewModel(IEventAggregator eventAggregator, IGameState gameState)
        {
            _eventAggregator = eventAggregator;
            _gameState = gameState;
            DisplayName = "Modes";
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
        public void Handle(UpdateModesEvent message)
        {
            BuildTree();
        }

        private void BuildTree() 
        {
            _modeItems = new ObservableCollection<ModeItemViewModel>();

            foreach (var mode in _gameState.Modes)
            {
                var modeEvents = new ObservableCollection<ModeEventViewModel>();
                foreach (var modeEventConfig in mode.ModeEvents)
                {
                    var eventTrigger = new ModeEventViewModel(_gameState, "Event", modeEventConfig.Name, 1, modeEventConfig.AssociatedSwitchName);
                    modeEvents.Add(eventTrigger);
                }

                var modeRequiredDevices = new ObservableCollection<ModeRequiredDeviceViewModel>();
                foreach (var requiredDeviceConfig in mode.RequiredDevices)
                {
                    var requiredDevice = new ModeRequiredDeviceViewModel(_gameState, requiredDeviceConfig.TypeOfDevice, requiredDeviceConfig.Name, 1, requiredDeviceConfig.DeviceName);
                    modeRequiredDevices.Add(requiredDevice);
                }

                _modeItems.Add(new ModeItemViewModel(mode.Title, modeEvents, modeRequiredDevices, null));
            }

            NotifyOfPropertyChange(() => ModeItems);

        }

        private ObservableCollection<ModeItemViewModel> _modeItems;
        private readonly IEventAggregator _eventAggregator;
        private readonly IGameState _gameState;

    }
}
