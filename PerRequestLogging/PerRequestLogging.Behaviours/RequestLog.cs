using System;
using System.ServiceModel;

namespace PerRequestLogging.Behaviours
{
    public class RequestLog : IExtension<OperationContext>
    {
        public string RequestIdentifier { get; internal set; }

        public static RequestLog Current
        {
            get
            {
                var instance = OperationContext.Current.Extensions.Find<RequestLog>();
                if (instance == null)
                {
                    instance = new RequestLog();
                    OperationContext.Current.Extensions.Add(instance);
                }
                return instance;
            }
        }

        public void Attach(OperationContext owner) { }

        public void Detach(OperationContext owner) { }
    }
}
