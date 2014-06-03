using System;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace PerRequestLogging.Behaviours
{
    public class RequestLoggingInspector : IParameterInspector
    {
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            RequestLog.Current.RequestIdentifier = Guid.NewGuid().ToString();
            return null;
        }
    }
}
