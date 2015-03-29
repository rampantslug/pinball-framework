using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary.Logging
{
    public class RsLogManager
    {
        //public static ILog GetCurrent

        private IServerBusController _busController;

        private static readonly Lazy<RsLogManager> ClassInstance = new Lazy<RsLogManager>(() => new RsLogManager());

        private RsLogManager()
	    {
            
	    }
	
        /// <summary>
        /// Use the active logger for log message
        /// </summary>
        public static RsLogManager GetCurrent
	    {
		    get { return ClassInstance.Value; }
	    }

        public void LogTestMessage(string message)
        {
            if (_busController == null)
            {
                _busController = IoC.Get<IServerBusController>();
            }
            _busController.SendEventMessage(message);
        }
    }
}
