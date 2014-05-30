using System.Runtime.Serialization;

namespace CleanWsdlNS.ValueObjects
{
    [DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public class HelloResponse
    {
        public string Result { get; set; }
    }
}
