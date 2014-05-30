using System.ServiceModel;
using FilelessActivation.ValueObjects;

namespace FilelessActivation.Services
{
    [ServiceContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public interface IHelloService
    {
        [OperationContract]
        HelloResponse SayHello(HelloRequest request);
    }
}
