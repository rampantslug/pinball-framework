
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using System.Linq;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.ComponentModel.Composition;
    using System.Reflection;
    using Common;
    using Hardware.Arduino;
    using Hardware.Proc;


namespace ServerLibrary
    {
        public class ServerLibraryBootstrapper
        {
            /// <summary>
            /// Create an instance of the game library by adding available Controllers to 
            /// IoC container.
            /// </summary>
            /// <param name="container"></param>
            public ServerLibraryBootstrapper(SimpleContainer container)
            {
                container.Singleton<IServerBusController, ServerBusController>();
                //container.Singleton<IRsLogger, RsLogger>();
                
                // Retrieve all modes from Server and Local
                container.AllTypesOf<IMode>(Assembly.GetExecutingAssembly());
                container.AllTypesOf<IMode>(Assembly.GetEntryAssembly());
                
                // TODO: Modify this to function similar to IMode but instead use IHardwareController
                container.Singleton<IProcController, ProcController>();
                container.Singleton<IArduinoController, ArduinoController>();

                container.Singleton<IDevices, Devices>();


                container.Singleton<IGameController, GameController>();
                
                // Add Display elements
               // container.Singleton<IDisplayBackgroundVideo, BackgroundVideoViewModel>();
               // container.Singleton<IDisplayMainScore, MainScoreViewModel>();
            }   
        }
    }
