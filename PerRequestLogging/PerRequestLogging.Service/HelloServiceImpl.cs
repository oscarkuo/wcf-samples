using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace PerRequestLogging.Service
{
    [ServiceContract(Namespace = "http://oscarkuo.com/v1/calculator")]
    [ServiceBehavior(Namespace = "http://oscarkuo.com/v1/calculator")]
    public class HelloServiceImpl
    {
        [OperationContract]
        public HelloResponse SayHello(HelloRequest request)
        {
            return new HelloResponse { Result = "Hello " + request.Name };
        }
    }
}