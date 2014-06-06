using System.Runtime.Serialization;


namespace CleanWsdlNS.ValueObjects
{
    [DataContract]
    public class HelloRequest
    {
        [DataMember]
        public string Name { get; set; }
    }
}
