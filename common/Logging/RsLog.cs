using System.Diagnostics;

namespace Logging
{
    public class RsLog
    {
        //private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public static void LogMessage(LogEventType eventType, OriginatorType originator, string originatorName, string status, string information)
        {
            RsLogger.LogMessage(eventType, originator, originatorName, status, information);
        }

        public static void Info(string format, params object[] formatArugments)
        {
            RsLogger.Info(GetSource(), format, formatArugments);
        }

        public static void Debug(string format, params object[] formatArugments)
        {
            RsLogger.Debug(GetSource(), format, formatArugments);
        }

        public static void Warn(string format, params object[] formatArugments)
        {
            RsLogger.Warn(GetSource(), format, formatArugments);
        }

        public static void Error(string format, params object[] formatArugments)
        {
            RsLogger.Error(GetSource(), format, formatArugments);
        }

        private static string GetSource()
        {
            int depth = 2;

            var stackTrace = new StackTrace();
            if (stackTrace.FrameCount <= depth)
            {
                depth = stackTrace.FrameCount - 1;
            }
            var method = stackTrace.GetFrame(depth)?.GetMethod();
            if (null == method)
            {
                return "";
            }

            var typeName = method.ReflectedType?.Name ?? "";

            return typeName + "." + method.Name;
        }


        private static IRsLogger RsLogger
        {
            get
            {
                if (null == _rsLogger)
                {
                    _rsLogger = new FileLogger();
                }

                return _rsLogger;
            }
        }

        public void SetDefaultLogger(IRsLogger defaultLogger)
        {
            _rsLogger = defaultLogger;
        }

        private static IRsLogger _rsLogger;

    }
}
