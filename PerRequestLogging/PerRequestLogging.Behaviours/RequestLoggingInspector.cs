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
    public class RequestLoggingInspector : IDispatchMessageInspector
    {
        private Message CopyLogMessage(MessageBuffer buffer)
        {
            RequestLoggingExtension.Current.Log.WriteMessage(buffer.CreateMessage());
            return buffer.CreateMessage();
        } 

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            RequestLoggingExtension.Current.BeginRequest();
            request = CopyLogMessage(request.CreateBufferedCopy(int.MaxValue));
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            reply = CopyLogMessage(reply.CreateBufferedCopy(int.MaxValue));
            RequestLoggingExtension.Current.CompleteRequest();
        }
    }
}
