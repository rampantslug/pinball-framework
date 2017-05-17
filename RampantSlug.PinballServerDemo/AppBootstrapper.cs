using System.Reflection;

namespace RampantSlug.PinballServerDemo {
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using RampantSlug.ServerLibrary;

    public class AppBootstrapper : BootstrapperBase {
        SimpleContainer container;
        private ServerLibraryBootstrapper _serverLibrary;

        public AppBootstrapper() {
            Initialize();
        }

        protected override void Configure() {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            // Create Game Library
            _serverLibrary = new ServerLibraryBootstrapper(container);
            container.PerRequest<IShell, ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key) {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<IShell>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] {
                Assembly.GetExecutingAssembly(), 
                Assembly.LoadFrom("RampantSlug.ServerLibrary.dll")
            };
        }
    }
}