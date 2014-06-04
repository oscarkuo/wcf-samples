using System;
using PerRequestLogging.ValueObjects;
using System.ServiceModel.Channels;

namespace PerRequestLogging.Infrastructure
{
    public interface IRequestLog
    {
        Uri ServiceEndpointUri { get; set; }
        Guid InternalRequestIdentifier { get; set; }

        void WriteError(string message, Exception exception);
        void WriteWarning(string message);
        void WriteInformation(string message);
        void WriteMessage(Message message);

        void Flush();
    }
}
