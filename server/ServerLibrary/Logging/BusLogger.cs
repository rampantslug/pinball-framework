using System;
using Caliburn.Micro;
using Logging;

namespace ServerLibrary.Logging
{
    public class BusLogger : IRsLogger
    {
        //public static ILog GetCurrent

        private IServerBusController _busController;

        private static readonly Lazy<BusLogger> ClassInstance = new Lazy<BusLogger>(() => new BusLogger());

        public BusLogger()
	    {
            
	    }
	
        /// <summary>
        /// Use the active logger for log message
        /// </summary>
        public static IRsLogger GetCurrent
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

        public void Info(string source, string format, params object[] formatArguments)
        {
            throw new NotImplementedException();
        }

        public void Debug(string source, string format, params object[] formatArguments)
        {
            throw new NotImplementedException();
        }

        public void Warn(string source, string format, params object[] formatArguments)
        {
            throw new NotImplementedException();
        }

        public void Error(string source, string format, params object[] formatArguments)
        {
            throw new NotImplementedException();
        }
    }
}
