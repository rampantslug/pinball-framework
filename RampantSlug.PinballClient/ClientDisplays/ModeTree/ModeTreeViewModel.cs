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

namespace RampantSlug.PinballClient.ClientDisplays.ModeTree
{
    public class ModeTreeViewModel: Screen, IModeTree,
        IHandle<CommonViewModelsLoaded>
    {

        private ObservableCollection<ModeItemViewModel> _firstGeneration;
        private IEventAggregator _eventAggregator;

        /// <summary>
        /// Returns a read-only collection containing the first person 
        /// in the family tree, to which the TreeView can bind.
        /// </summary>
        public ObservableCollection<ModeItemViewModel> FirstGeneration
        {
            get { return _firstGeneration; }
        }

        [ImportingConstructor]
        public ModeTreeViewModel()
        {
            DisplayName = "Modes";

            BuildTree();
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
           
            _firstGeneration = new ObservableCollection<ModeItemViewModel>();

            _firstGeneration.Add(new ModeItemViewModel("Attract", new ObservableCollection<SwitchHandlerViewModel>()
            {
                new SwitchHandlerViewModel("Start Game", 33, "startButton")
            }));

            _firstGeneration.Add(new ModeItemViewModel("Trough", new ObservableCollection<SwitchHandlerViewModel>()
            {
                new SwitchHandlerViewModel("Ball Trough 1", 49, "trough1"),
                new SwitchHandlerViewModel("Ball Trough 2", 50, "trough2"),
                new SwitchHandlerViewModel("Ball Trough 3", 51, "trough3"),
                new SwitchHandlerViewModel("Ball Trough 4", 52, "trough4"),
                new SwitchHandlerViewModel("Ball Trough 5", 53, "trough5")
            }));

            _firstGeneration.Add(new ModeItemViewModel("BaseGame", new ObservableCollection<SwitchHandlerViewModel>()
            {
                new SwitchHandlerViewModel("Score Small Points", 70, "lowerTarget"),
                new SwitchHandlerViewModel("Score Big Points", 71, "middleTarget")
            }));

            _firstGeneration.Add(new ModeItemViewModel("Multiball 1", new ObservableCollection<SwitchHandlerViewModel>()
            {
                new SwitchHandlerViewModel("Start Multiball", 99, "leftOutlane")
            }));

            _firstGeneration.Add(new ModeItemViewModel("Hurry Up", new ObservableCollection<SwitchHandlerViewModel>()
            {
                new SwitchHandlerViewModel("Start Hurr Up", 101, "rightOutlane")
            }));

            NotifyOfPropertyChange(() => FirstGeneration);

        }

    }
}
