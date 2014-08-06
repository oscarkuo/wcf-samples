using Ninject;
using Ninject.Extensions.Wcf;

using System;
using System.ServiceModel;

using Behaviours;

namespace UnitTests
{
    public static class Tools
    {
        private static void InvokeOperation<I>(string endpointAddress, Action<I> invokeClientOperationCallback)
        {
            var client = new ChannelFactory<I>(new NetTcpBinding(), new EndpointAddress(endpointAddress)).CreateChannel();
            var commObj = (ICommunicationObject)client;

            try
            {
                invokeClientOperationCallback(client);
                commObj.Close();
            }
            catch
            {
                commObj.Abort();
                throw;
            }
        }

        public static void StartAndInvokeService<I, S, L, T>(string baseAddress, Action<I> invokeClientOperationCallback)
            where S : I
            where L : IInteractionLog
            where T : IInteractionState
        {
            if (invokeClientOperationCallback == null)
                throw new ArgumentNullException("invokeClientOperationCallback");

            var baseAddressUri = new Uri(baseAddress);
            NinjectServiceSelfHostFactory.SetKernel(new StandardKernel(new UnitTestNinjectModule<L, T>()));
            using (var host = new NinjectServiceSelfHostFactory().CreateServiceHost(typeof(S), new[] { baseAddressUri }))
            {
                var endpoint = host.AddServiceEndpoint(typeof(I), new NetTcpBinding(), "");
                endpoint.EndpointBehaviors.Add(new InteractionLoggingEndpointBehaviour<UnitTestNinjectModule<L, T>>());
                host.Open();
                InvokeOperation(baseAddress, invokeClientOperationCallback);
                host.Close();
            }
        }
    }
}
