using PerRequestLogging.ValueObjects;
using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace PerRequestLogging.Behaviours
{
    public class InteractionLoggingInspector : IDispatchMessageInspector
    {
        private IInteractionLog _log;
        private IInteractionState _state;

        public InteractionLoggingInspector(IInteractionLog log, IInteractionState state)
        {
            _log = log;
            _state = state;
        }

        private Message CopyLogMessage(MessageBuffer buffer)
        {
            _log.WriteMessage(buffer.CreateMessage());
            return buffer.CreateMessage();
        } 

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            _state.Set("ServiceRequestUri", OperationContext.Current.Channel.LocalAddress.Uri.ToString());
            _state.Set("InternalCorrelationIdentifier", Guid.NewGuid().ToString());
            request = CopyLogMessage(request.CreateBufferedCopy(int.MaxValue));
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            reply = CopyLogMessage(reply.CreateBufferedCopy(int.MaxValue));
            _log.Flush();
        }
    }
}
