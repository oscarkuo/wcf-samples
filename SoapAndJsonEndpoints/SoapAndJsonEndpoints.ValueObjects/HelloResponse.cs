using System;
using System.Runtime.Serialization;

namespace SoapAndJsonEndpoints.ValueObjects
{
    [DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
    public class HelloResponse : Message
    {
        [DataMember]
        public string Result { get; set; }
    }
}

