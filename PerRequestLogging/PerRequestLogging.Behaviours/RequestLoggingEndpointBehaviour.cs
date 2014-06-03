using System;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace PerRequestLogging.Behaviours
{
    public class RequestLoggingEndpointBehaviour : BehaviorExtensionElement, IEndpointBehavior
    {
        public override Type BehaviorType
        {
            get { return typeof(RequestLoggingEndpointBehaviour); }
        }

        protected override object CreateBehavior()
        {
            return new RequestLoggingEndpointBehaviour();
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            foreach (var operation in endpointDispatcher.DispatchRuntime.Operations)
            {
                operation.ParameterInspectors.Add(new RequestLoggingInspector());
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
