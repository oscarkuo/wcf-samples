using LogMessageWithBehaviour.ValueObjects;
using System.ServiceModel;


namespace LogMessageWithBehaviour.Services
{
    [ServiceBehavior(Namespace = "http://oscarkuo.com/v1/hello")]
    public class HelloServiceImpl : IHelloService, IHelloServiceJson
    {
        public HelloResponse SayHello(HelloRequest request)
        {
            return new HelloResponse { Result = "Hello " + request.Name, CorrelationIdentifier = request.CorrelationIdentifier };
        }

        public HelloResponse SayHello(string name, string correlationId)
        {
            return SayHello(new HelloRequest { Name = name, CorrelationIdentifier = correlationId });
        }
    }
}