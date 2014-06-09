using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using PerRequestLogging.ValueObjects;
using System.Threading;
using PerRequestLogging.Behaviours;
using System.Diagnostics;

namespace PerRequestLogging.Services
{
    [ServiceContract(Namespace = "http://oscarkuo.com/v1/calculator")]
    [ServiceBehavior(Namespace = "http://oscarkuo.com/v1/calculator")]
    public class HelloServiceImpl
    {
        private IInteractionLog _log;

        public HelloServiceImpl(IInteractionLog log) // constructor injection by ninject
        {
            _log = log;
        }

        [OperationContract]
        public HelloResponse SayHello(string token, HelloRequest request)
        {
            _log.WriteInformation("Entering SayHello()");

            var r = new Random().Next(1, 5);
            _log.WriteInformation("Sleeping for " + r + " seconds");
            Thread.Sleep(r * 1000); // for multi-thread testing

            _log.WriteInformation("Leaving SayHello()");

            return new HelloResponse { Result = "Hello " + request.Name };
        }
    }
}