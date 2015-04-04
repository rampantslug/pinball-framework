
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
        public class GameLibraryBootstrapper
        {
            /// <summary>
            /// Create an instance of the game library by adding available Controllers to 
            /// IoC container.
            /// </summary>
            /// <param name="container"></param>
            public GameLibraryBootstrapper(SimpleContainer container)
            {
                container.Singleton<IServerBusController, ServerBusController>();
                container.Singleton<IProcController, ProcController>();
                container.Singleton<IArduinoController, ArduinoController>();
                container.Singleton<IGameController, GameController>();
            }   
        }
    }
