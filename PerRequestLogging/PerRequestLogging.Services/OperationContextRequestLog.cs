using PerRequestLogging.Behaviours;
using PerRequestLogging.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerRequestLogging.Services
{
    public class OperationContextRequestLog : IRequestLog
    {
        public Uri ServiceEndpointUri
        {
            get { return RequestLoggingExtension.Current.Log.ServiceEndpointUri; }
            set { RequestLoggingExtension.Current.Log.ServiceEndpointUri = value; }        
        }

        public Guid InternalRequestIdentifier
        {
            get { return RequestLoggingExtension.Current.Log.InternalRequestIdentifier; }
            set { RequestLoggingExtension.Current.Log.InternalRequestIdentifier = value; }
        }

        public void WriteError(string message, Exception exception)
        {
            RequestLoggingExtension.Current.Log.WriteError(message, exception);
        }

        public void WriteWarning(string message)
        {
            RequestLoggingExtension.Current.Log.WriteWarning(message);
        }

        public void WriteInformation(string message)
        {
            RequestLoggingExtension.Current.Log.WriteInformation(message);
        }

        public void WriteMessage(System.ServiceModel.Channels.Message message)
        {
            RequestLoggingExtension.Current.Log.WriteMessage(message);
        }

        public void Flush()
        {
            RequestLoggingExtension.Current.Log.Flush();
        }
    }
}