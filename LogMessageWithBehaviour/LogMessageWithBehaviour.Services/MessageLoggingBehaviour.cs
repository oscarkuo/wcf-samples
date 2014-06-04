using System;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;


namespace LogMessageWithBehaviour.Services
{
    public class MessageLoggingBehaviour : BehaviorExtensionElement, IEndpointBehavior
    {
        public override Type BehaviorType
        {
            get { return typeof(MessageLoggingBehaviour); }
        }

        protected override object CreateBehavior()
        {
            return new MessageLoggingBehaviour();
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new MessageLoggingInspector());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}