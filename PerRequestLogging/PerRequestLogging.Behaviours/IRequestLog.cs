using System;
using PerRequestLogging.ValueObjects;

namespace PerRequestLogging.Behaviours
{
    public interface IRequestLog
    {
        void LogFailure(string message, Exception exception);
        void LogWarning(string message);
        void LogMessage(string message);
        void LogPayload(Payload payload);
    }
}
