using System;

namespace RampantSlug.Common.Logging
{
    // Summary:
    //     A logger.
    public interface ILog
    {
        // Summary:
        //     Logs the exception.
        //
        // Parameters:
        //   exception:
        //     The exception.
        void Error(Exception exception);
        //
        // Summary:
        //     Logs the message as info.
        //
        // Parameters:
        //   format:
        //     A formatted message.
        //
        //   args:
        //     Parameters to be injected into the formatted message.
        void Info(string format, params object[] args);
        //
        // Summary:
        //     Logs the message as a warning.
        //
        // Parameters:
        //   format:
        //     A formatted message.
        //
        //   args:
        //     Parameters to be injected into the formatted message.
        void Warn(string format, params object[] args);
    }
}
