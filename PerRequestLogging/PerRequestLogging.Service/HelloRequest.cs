using System.Runtime.Serialization;

namespace PerRequestLogging.Service
{
    [DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public class HelloRequest
    {
        [DataMember]
        public string Name { get; set; }
    }
}