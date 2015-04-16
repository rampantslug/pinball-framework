using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.ContractImplementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RampantSlug.Common;
using RampantSlug.PinballClient.CommonViewModels;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree
{
    public class DeviceTreeViewModel: Screen, IDeviceTree, 
        IHandle<CommonViewModelsLoaded>
    {
        public string ClientDisplayName { get { return "Device Tree"; } }

        private ObservableCollection<DeviceTypeViewModel> _firstGeneration;
        private IEventAggregator _eventAggregator;

        /// <summary>
        /// Returns a read-only collection containing the first person 
        /// in the family tree, to which the TreeView can bind.
        /// </summary>
        public ObservableCollection<DeviceTypeViewModel> FirstGeneration
        {
            get { return _firstGeneration; }
        }

        [ImportingConstructor]
        public DeviceTreeViewModel()
        {
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);
            
        }

        /// <summary>
        /// Update tree based on received settings
        /// </summary>
        /// <param name="message"></param>
        public void Handle(CommonViewModelsLoaded message)
        {
            BuildTree();
        }

        private void BuildTree() 
        {
            var shellViewModel = IoC.Get<IShell>();
            
            _firstGeneration = new ObservableCollection<DeviceTypeViewModel>();

            _firstGeneration.Add(new DeviceTypeViewModel(new SwitchType("Switches"), new ObservableCollection<DeviceViewModel>(shellViewModel.Switches)));

            _firstGeneration.Add(new DeviceTypeViewModel(new CoilType("Coils"), new ObservableCollection<DeviceViewModel>(shellViewModel.Coils)));

            _firstGeneration.Add(new DeviceTypeViewModel(new StepperMotorType("Stepper Motors"), new ObservableCollection<DeviceViewModel>(shellViewModel.StepperMotors)));

            _firstGeneration.Add(new DeviceTypeViewModel(new ServoType("Servos"), new ObservableCollection<DeviceViewModel>(shellViewModel.Servos)));    

            NotifyOfPropertyChange(() => FirstGeneration);

        }

    }
}
