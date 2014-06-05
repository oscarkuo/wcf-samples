using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace LogMessageWithBehaviour.Services
{
    public class MessageLoggingInspector : IDispatchMessageInspector
    {
        private static Message CopyAndLogMessage(Log log, MessageBuffer buffer)
        {
            var message = buffer.CreateMessage();
            log.Write(message);
            return buffer.CreateMessage();
        }

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var buffer = request.CreateBufferedCopy(int.MaxValue);
            var path = Path.Combine(Path.GetTempPath(), "LogMessageWithBehaviour.txt");
            var log = new Log(path, request.Headers.To);
            request = CopyAndLogMessage(log, buffer);
            return log;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            var log = (Log)correlationState;
            reply = CopyAndLogMessage(log, reply.CreateBufferedCopy(int.MaxValue));
            log.Flush();
        }
    }
}