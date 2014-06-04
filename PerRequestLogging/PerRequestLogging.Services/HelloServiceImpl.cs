using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using PerRequestLogging.ValueObjects;
using System.Threading;
using PerRequestLogging.Behaviours;

namespace PerRequestLogging.Services
{
    [ServiceContract(Namespace = "http://oscarkuo.com/v1/calculator")]
    [ServiceBehavior(Namespace = "http://oscarkuo.com/v1/calculator")]
    public class HelloServiceImpl
    {
        [OperationContract]
        public HelloResponse SayHello(string token, HelloRequest request)
        {
            RequestLoggingExtension.Current.Log.WriteInformation("Begin SayHello()");

            var r = new Random().Next(1, 5);
            Thread.Sleep(r * 1000); // for multi-thread testing
            if ((r % 2) == 0)
            {
                throw new FaultException("Uh-oh"); // to test logging exceptions 
            }

            RequestLoggingExtension.Current.Log.WriteInformation("Complete SayHello()");
            return new HelloResponse { Result = "Hello " + request.Name };
        }
    }
}