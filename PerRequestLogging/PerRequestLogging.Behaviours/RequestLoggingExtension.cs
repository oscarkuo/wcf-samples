using PerRequestLogging.Infrastructure;
using PerRequestLogging.ValueObjects;
using System;
using System.IO;
using System.ServiceModel;
using System.Threading;

namespace PerRequestLogging.Behaviours
{
    public class RequestLoggingExtension : IExtension<OperationContext>
    {
        public IRequestLog Log { get; private set; }

        private void CheckThrowOnNullRequestLog()
        {
            if (Log == null)
            {
                throw new InvalidOperationException("No log object created, did you forget to call BeginRequest()?");
            }
        }

        public void BeginRequest()
        {
            Log = new FileRequestLog();
            Log.ServiceEndpointUri = OperationContext.Current.Channel.LocalAddress.Uri;
            Log.InternalRequestIdentifier = Guid.NewGuid();
        }

        public void CompleteRequest()
        {
            CheckThrowOnNullRequestLog();
            Log.Flush();
            Log = null;
        }

        public static RequestLoggingExtension Current
        {
            get
            {
                var instance = OperationContext.Current.Extensions.Find<RequestLoggingExtension>();
                if (instance == null)
                {
                    instance = new RequestLoggingExtension();
                    OperationContext.Current.Extensions.Add(instance);
                }

                return instance;
            }
        }

        public void Attach(OperationContext owner) { }
        public void Detach(OperationContext owner) { }
    }
}
