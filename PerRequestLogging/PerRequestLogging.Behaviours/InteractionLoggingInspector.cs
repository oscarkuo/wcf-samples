using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

using PerRequestLogging.ValueObjects;

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
            // set the required state for logging, which is a shared IDictionary stored in OperationContext.Current (See WcfInteractionState.cs)
            _state.Set(Constants.ServiceRequestUriKey, request.Headers.To);
            _state.Set(Constants.InternalCorrelationIdentifierKey, Guid.NewGuid());
            _state.Set(Constants.InteractionLogBufferKey, new List<string>());
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
