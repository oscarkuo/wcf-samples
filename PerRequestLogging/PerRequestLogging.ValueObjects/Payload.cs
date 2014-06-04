using System.Runtime.Serialization;

namespace PerRequestLogging.ValueObjects
{
    [DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
    [KnownType(typeof(HelloRequest))]
    [KnownType(typeof(HelloResponse))]
    public class Payload
    {
        [DataMember]
        public string Application { get; set; }

        [DataMember]
        public string CorrelationIdentifer { get; set; }
    }
}
