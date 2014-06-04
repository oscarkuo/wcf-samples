using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using LogMessageWithBehaviour.ValueObjects;

namespace LogMessageWithBehaviour.Services
{
    [ServiceContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public interface IHelloServiceJson
    {
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "SayHelloTo/{name}?correlationId={correlationId}")]
        [OperationContract(Name = "SayHelloJson")]
        HelloResponse SayHello(string name, string correlationId);
    }
}
