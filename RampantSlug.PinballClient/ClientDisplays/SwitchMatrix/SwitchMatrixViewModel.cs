using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common.DeviceAddress;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ClientDisplays.SwitchMatrix
{
    [Export(typeof(SwitchMatrixViewModel))]
    public sealed class SwitchMatrixViewModel : Screen, ISwitchMatrix, IHandle<CommonViewModelsLoaded>
    {
        private ObservableCollection<SwitchMatrixItemViewModel> _switches;
        private IEventAggregator _eventAggregator;
       

        public ObservableCollection<SwitchMatrixItemViewModel> Switches
        {
            get { return _switches; }
            set
            {
                _switches = value;
                NotifyOfPropertyChange(() => Switches);
            }
        }

        public SwitchMatrixViewModel()
        {
            DisplayName = "Switch Matrix";
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);

            RebuildMatrix();
        }

        /// <summary>
        /// Update Switch Matrix based on received settings
        /// </summary>
        /// <param name="message"></param>
        public void Handle(CommonViewModelsLoaded message)
        {
            RebuildMatrix();
        }

        private void RebuildMatrix()
        {
            var shellViewModel = IoC.Get<IShell>();
            Switches = new ObservableCollection<SwitchMatrixItemViewModel>();

            foreach (var switchViewModel in shellViewModel.Switches)
            {
                if (switchViewModel.Address.HardwareType == AddressFactory.HardwareType.ProcSwitchMatrix)
                {
                    Switches.Add(new SwitchMatrixItemViewModel(switchViewModel));
                }
            }
            
        }
    }
}
