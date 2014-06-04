using System;
using System.Runtime.Serialization;

namespace LogMessageWithBehaviour.ValueObjects
{
    [DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public class Message
    {
        [DataMember]
        public string CorrelationIdentifier { get; set; }
    }
}
