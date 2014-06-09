using Ninject;
using Ninject.Modules;
using System;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace PerRequestLogging.Behaviours
{
    public class InteractionLoggingEndpointBehaviour<T> : BehaviorExtensionElement, IEndpointBehavior
        where T : INinjectModule, new()
    {
        public override Type BehaviorType
        {
            get { return typeof(InteractionLoggingEndpointBehaviour<T>); }
        }

        protected override object CreateBehavior()
        {
            return new InteractionLoggingEndpointBehaviour<T>();
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            var kernel = new StandardKernel(new T());
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(kernel.Get<InteractionLoggingInspector>());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
