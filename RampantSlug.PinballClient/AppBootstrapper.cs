namespace RampantSlug.PinballClient {
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using System.Linq;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.ComponentModel.Composition;
    using System.Reflection;
    using MassTransit;
    using RampantSlug.Common;
    using RampantSlug.PinballClient.ContractImplementations;
    using RampantSlug.PinballClient.ClientDisplays.LogMessages;
    using RampantSlug.PinballClient.ClientDisplays.DeviceConfiguration;
    using RampantSlug.PinballClient.ClientDisplays.SwitchMatrix;
    using RampantSlug.PinballClient.ClientDisplays.DeviceTree;


    public class AppBootstrapper : BootstrapperBase 
    {
        
        //SimpleContainer container;

        protected CompositionContainer _container;

        public AppBootstrapper() 
        {
             Initialize();
           
        }

        protected override void Configure() 
        {
            //container = new SimpleContainer();

            //container.Singleton<IWindowManager, WindowManager>();
            //container.Singleton<IEventAggregator, EventAggregator>();
            //container.PerRequest<IShell, ShellViewModel>();

            //var catalog = new AggregateCatalog(
            //   AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
            //   );
           /* AggregateCatalog catalog =
   new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(
               Assembly.GetExecutingAssembly()));
            */
            /*_container = CompositionHost.Initialize(
            new AggregateCatalog(
                AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
                )
            );*/

            _container = new CompositionContainer (  new AggregateCatalog (  AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog >() ));

            //_container = new CompositionContainer(catalog);

            var batch = new CompositionBatch();
            var window = new WindowManager();
 

            batch.AddExportedValue<IWindowManager>(window);
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue<IClientBusController>(new ClientBusController());

            // Add Client Displays
            batch.AddExportedValue<ILogMessages>(new LogMessagesViewModel());
            batch.AddExportedValue<IDeviceConfiguration>(new DeviceConfigurationViewModel());
            batch.AddExportedValue<ISwitchMatrix>(new SwitchMatrixViewModel());
            batch.AddExportedValue<IDeviceTree>(new DeviceTreeViewModel());

            batch.AddExportedValue(_container);
            //batch.AddExportedValue(catalog);

            _container.Compose(batch);
        }

       /* protected override object GetInstance(Type service, string key) {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }*/

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = _container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] {
        Assembly.GetExecutingAssembly()
    };
        }

       /* protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }*/

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<IShell>();
        }
    }
}