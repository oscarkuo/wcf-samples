using System.Runtime.Serialization;

namespace CleanWsdlNS.ValueObjects
{
    [DataContract]
    public class HelloResponse
    {
        [DataMember]
        public string Result { get; set; }
    }
}
