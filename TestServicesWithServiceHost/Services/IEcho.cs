using System;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IEcho
    {
        [OperationContract]
        string Echo(string value);
    }
}
