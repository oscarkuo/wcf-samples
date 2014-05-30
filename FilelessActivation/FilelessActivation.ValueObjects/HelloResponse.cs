using System.Runtime.Serialization;

namespace FilelessActivation.ValueObjects
{
    [DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public class HelloResponse
    {
        [DataMember]
        public string Result { get; set; }
    }
}
