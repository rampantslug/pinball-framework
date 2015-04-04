
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using System.Linq;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.ComponentModel.Composition;
    using System.Reflection;
    using RampantSlug.ServerLibrary.Hardware;
    using RampantSlug.ServerLibrary.Hardware.Proc;

namespace RampantSlug.ServerLibrary
    {
       /* public class AppBootstrapper : BootstrapperBase
        {
            protected CompositionContainer Container;
            public IGameController GameController { get; private set; }

            public AppBootstrapper()
            {
                Initialize();

            }

            

            protected override void Configure()
            {
                Container = new CompositionContainer(new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));

                var batch = new CompositionBatch();
                //var window = new WindowManager();

                //batch.AddExportedValue<IWindowManager>(window);

                var gameController = new GameController();
                GameController = gameController;
                batch.AddExportedValue<IGameController>(gameController);

                batch.AddExportedValue<IEventAggregator>(new EventAggregator());
                batch.AddExportedValue<IServerBusController>(new ServerBusController());
                batch.AddExportedValue<IProcController>(new ProcController());
                batch.AddExportedValue<IArduinoController>(new ArduinoController());

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

       
        }*/
    }
