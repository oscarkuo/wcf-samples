using System.Runtime.Serialization;

namespace PerRequestLogging.ValueObjects
{
    [DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public class HelloRequest : Payload
    {
        [DataMember]
        public string Name { get; set; }
    }
}
