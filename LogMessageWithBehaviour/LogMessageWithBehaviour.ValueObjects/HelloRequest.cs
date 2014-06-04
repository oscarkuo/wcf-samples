using System;
using System.Runtime.Serialization;

namespace LogMessageWithBehaviour.ValueObjects
{
    [DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public class HelloRequest : Message
    {
        [DataMember]
        public string Name { get; set; }
    }
}
