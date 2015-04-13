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
using RampantSlug.PinballClient.ClientDisplays.DeviceTree.DeviceTypes;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree
{
    public class DeviceTreeViewModel: Screen, IDeviceTree, IHandle<ConfigResults>, IHandle<DeviceChange>
    {
        public string ClientDisplayName { get { return "Device Tree"; } }

        private ObservableCollection<DeviceTypeViewModel> _firstGeneration;
        private IEventAggregator _eventAggregator;
        IEnumerator<DeviceItemViewModel> _matchingDeviceEnumerator;

        string _searchText = string.Empty;
        private ushort _searchId;


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

        private void RebuildTree(Configuration configuration) 
        {

            
            _firstGeneration = new ObservableCollection<DeviceTypeViewModel>();

            // Add Switches
            var swDevices = configuration.Switches.Cast<IDevice>().ToList();
            _firstGeneration.Add(new DeviceTypeViewModel(new SwitchType("Switches"), swDevices));

            // Add Coils
            var swCoils = configuration.Coils.Cast<IDevice>().ToList();
            _firstGeneration.Add(new DeviceTypeViewModel(new CoilType("Coils"), swCoils));

            // Add Servos
            var swServos = configuration.Servos.Cast<IDevice>().ToList();
            _firstGeneration.Add(new DeviceTypeViewModel(new ServoType("Servos"), swServos));

            // Add Steppers
            var swSteppers = configuration.StepperMotors.Cast<IDevice>().ToList();
            _firstGeneration.Add(new DeviceTypeViewModel(new StepperMotorType("Stepper Motors"), swSteppers));

            // Add DC Motors
            var swDCMotors = configuration.DCMotors.Cast<IDevice>().ToList();
            _firstGeneration.Add(new DeviceTypeViewModel(new DCMotorType("DC Motors"), swDCMotors));

            // Add LEDs
            var swLeds = configuration.Leds.Cast<IDevice>().ToList();
            _firstGeneration.Add(new DeviceTypeViewModel(new LedType("Leds"), swLeds));          

            NotifyOfPropertyChange(() => FirstGeneration);

        }

        /// <summary>
        /// Update tree based on received settings
        /// </summary>
        /// <param name="message"></param>
        public void Handle(ConfigResults message)
        {
            RebuildTree(message.MachineConfiguration);
        }

        /// <summary>
        /// Update single device based on notification
        /// </summary>
        /// <param name="message"></param>
        public void Handle(DeviceChange message)
        {
            var testSw = message.Device as Switch;

            
            var deviceViewModel = FindDeviceViewModelById(message.Device);
            if (deviceViewModel != null)
            {
                var swVM = deviceViewModel as SwitchViewModel;
                if (swVM != null)
                {
                    swVM.Device = message.Device;
                    swVM.Refresh();
                    //NotifyOfPropertyChange(() => swVM.SwitchState);
                }
                //deviceViewModel.Device = message.Device;
            }
        }

       

        public void ConfigureDevice() 
        {
           // _eventAggregator.PublishOnUIThread(new ConfigureDevice{Device = })
        }


        #region Search Logic

        private DeviceItemViewModel FindDeviceViewModelById(IDevice device)
        {
            foreach (var deviceType in FirstGeneration)
            {
                foreach (var deviceViewModel in deviceType.Children)
                {
                    if (deviceViewModel.Number == device.Number)
                    {
                        return deviceViewModel;
                    }
                }
            }
            return null;
        }


/*
        public void PerformSearch(IDevice device)
        {
            if (_matchingDeviceEnumerator == null || !_matchingDeviceEnumerator.MoveNext())
                this.VerifyMatchingPeopleEnumerator();

            var deviceViewModel = _matchingDeviceEnumerator.Current;

            if (deviceViewModel == null)
                return;

            deviceViewModel.Device = device;

            // Ensure that this person is in view.
            // if (person.Parent != null)
            //     person.Parent.IsExpanded = true;

            // person.IsSelected = true;


        }

        void VerifyMatchingPeopleEnumerator()
        {
            //IEnumerable<DeviceItemViewModel> allMatches;
            foreach (var item in FirstGeneration)
            {
                var matches = this.FindMatches(_searchId, item);
                _matchingDeviceEnumerator = matches.GetEnumerator();

                if (_matchingDeviceEnumerator.MoveNext())
                {
                    // Cannot find device with matching ID
                    return;
                }
            }   
        }

        IEnumerable<DeviceItemViewModel> FindMatches(ushort deviceId, DeviceItemViewModel equipmentItem)
        {
            if (equipmentItem.Number == deviceId)
                yield return equipmentItem;

            //foreach (DeviceItemViewModel child in equipmentItem.Children)
                //foreach (DeviceItemViewModel match in this.FindMatches(deviceId, child))
                    //yield return match;
        }
        */
        #endregion // Search Logic

    }
}
