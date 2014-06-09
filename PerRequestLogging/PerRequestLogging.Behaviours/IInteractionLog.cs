using System;
using PerRequestLogging.ValueObjects;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Ninject;

namespace PerRequestLogging.Behaviours
{
    public interface IInteractionLog
    {
        void WriteError(string message, Exception exception);
        void WriteWarning(string message);
        void WriteInformation(string message);
        void WriteMessage(Message message);

        void Flush();
    }
}
