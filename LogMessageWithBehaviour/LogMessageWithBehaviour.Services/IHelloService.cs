using LogMessageWithBehaviour.ValueObjects;
using System;
using System.ServiceModel;


namespace LogMessageWithBehaviour.Services
{
    [ServiceContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public interface IHelloService
    {
        [OperationContract]
        HelloResponse SayHello(HelloRequest name);
    }
}
