using System;
using System.ServiceModel.Channels;

namespace Behaviours
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