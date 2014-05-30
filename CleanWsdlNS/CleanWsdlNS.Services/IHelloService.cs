using System.ServiceModel;
using CleanWsdlNS.ValueObjects;

namespace CleanWsdlNS.Services
{
    [ServiceContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public interface IHelloService
    {
        [OperationContract]
        HelloResponse SayHello(HelloRequest request);
    }
}
