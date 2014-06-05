using SoapAndJsonEndpoints.ValueObjects;

using System;
using System.ServiceModel;

namespace SoapAndJsonEndpoints.Services
{
    [ServiceContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public interface IHelloService
    {
        [OperationContract]
        HelloResponse SayHello(HelloRequest name);
    }
}
