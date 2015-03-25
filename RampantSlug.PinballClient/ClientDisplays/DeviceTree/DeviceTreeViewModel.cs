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
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree
{
    //[Export(typeof(IClientDisplay))]
    public class DeviceTreeViewModel: Screen, IDeviceTree, IHandle<SettingsResults>
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
           

         /*   var deviceTypes = TempConfig.GetDeviceTypes();
            _firstGeneration = new ObservableCollection<DeviceTypeViewModel>();

            foreach (var deviceType in deviceTypes)
            {
                _firstGeneration.Add(new DeviceTypeViewModel(deviceType));

            }
            NotifyOfPropertyChange(() => FirstGeneration);*/
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _eventAggregator = IoC.Get<IEventAggregator>();
            _eventAggregator.Subscribe(this);
        }

        private void RebuildTree(List<Switch> switches) 
        {
            //var deviceTypes = TempConfig.GetDeviceTypes();
            _firstGeneration = new ObservableCollection<DeviceTypeViewModel>();

            _firstGeneration.Add(new DeviceTypeViewModel(new CoilType("Coils"), new List<IDevice> {new Driver()
                            {
                                Name = "Coil 1",
                                Description = "test coil description"
                            }}));



            var swDevices = new List<IDevice>();
            foreach(Switch sw in switches)
            {
                swDevices.Add(sw);
            }
            _firstGeneration.Add(new DeviceTypeViewModel(new SwitchType("Switches"), swDevices));

            NotifyOfPropertyChange(() => FirstGeneration);
        }

        public void Handle(SettingsResults message)
        {
            RebuildTree(message.Switches);
        }

        public void ConfigureDevice() 
        {
           // _eventAggregator.PublishOnUIThread(new ConfigureDevice{Device = })
        }
    }
}
