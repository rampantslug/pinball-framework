using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using RampantSlug.PinballClient.ClientDisplays.DeviceInformation;
using RampantSlug.PinballClient.ClientDisplays.DeviceTree;
using RampantSlug.PinballClient.ClientDisplays.GameStatus;
using RampantSlug.PinballClient.ClientDisplays.LogMessages;
using RampantSlug.PinballClient.ClientDisplays.ModeTree;
using RampantSlug.PinballClient.ClientDisplays.Playfield;
using RampantSlug.PinballClient.ClientDisplays.SwitchMatrix;

namespace RampantSlug.PinballClient {
    public class AppBootstrapper : BootstrapperBase 
    {
        protected CompositionContainer Container;

        public AppBootstrapper() 
        {
             Initialize();
           
        }

        protected override void Configure() 
        {
            Container = new CompositionContainer (  new AggregateCatalog (  AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog >() ));

            var batch = new CompositionBatch();
            var window = new WindowManager();
 
            batch.AddExportedValue<IWindowManager>(window);
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue<IClientBusController>(new ClientBusController());

            // Add Client Displays
            batch.AddExportedValue<ILogMessages>(new LogMessagesViewModel());
            batch.AddExportedValue<IDeviceInformation>(new DeviceInformationViewModel());
            batch.AddExportedValue<ISwitchMatrix>(new SwitchMatrixViewModel());
            batch.AddExportedValue<IDeviceTree>(new DeviceTreeViewModel());
            batch.AddExportedValue<IGameStatus>(new GameStatusViewModel());
            batch.AddExportedValue<IPlayfield>(new PlayfieldViewModel());
            batch.AddExportedValue<IModeTree>(new ModeTreeViewModel());

            batch.AddExportedValue(Container);

            Container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = Container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return Container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            Container.SatisfyImportsOnce(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] {
        Assembly.GetExecutingAssembly()
    };
        }

        protected override void OnStartup(object sender, StartupEventArgs e) {
            DisplayRootViewFor<IShell>();
        }
    }
}