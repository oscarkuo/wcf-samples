using FilelessActivation.ValueObjects;
using System.ServiceModel;

namespace FilelessActivation.Services
{
    [ServiceBehavior(Namespace = "http://oscarkuo.com/v1/hello")]
    public class HelloServiceImpl : IHelloService
    {
        public HelloResponse SayHello(HelloRequest request)
        {
            return new HelloResponse { Result = "Hello " + request.Name };
        }
    }
}
