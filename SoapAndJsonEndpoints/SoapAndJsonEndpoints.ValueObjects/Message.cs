using System;
using System.Runtime.Serialization;

namespace SoapAndJsonEndpoints.ValueObjects
{
    [DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public class Message
    {
        [DataMember]
        public string CorrelationIdentifier { get; set; }
    }
}
