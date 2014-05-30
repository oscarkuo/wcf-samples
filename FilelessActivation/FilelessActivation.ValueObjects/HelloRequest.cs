using System.Runtime.Serialization;

namespace FilelessActivation.ValueObjects
{
    [DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public class HelloRequest
    {
        [DataMember]
        public string Name { get; set; }
    }
}
