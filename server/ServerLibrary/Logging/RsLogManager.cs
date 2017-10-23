using System;
using Caliburn.Micro;
using RampantSlug.Common.Logging;

namespace RampantSlug.ServerLibrary.Logging
{
    public class RsLogManager : IRsLogManager
    {
        //public static ILog GetCurrent

        private IServerBusController _busController;

        private static readonly Lazy<RsLogManager> ClassInstance = new Lazy<RsLogManager>(() => new RsLogManager());

        public RsLogManager()
	    {
            
	    }
	
        /// <summary>
        /// Use the active logger for log message
        /// </summary>
        public static IRsLogManager GetCurrent
	    {
		    get { return ClassInstance.Value; }
	    }

        public void LogMessage(LogEventType eventType, OriginatorType originator, string originatorName, string status, string information)
        {
            if (_busController == null)
            {
                _busController = IoC.Get<IServerBusController>();
            }
            _busController.SendLogMessage(eventType, originator, originatorName, status, information);
        }

        IRsLogManager IRsLogManager.GetCurrent
        {
            get { return GetCurrent; }
        }
    }
}
