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
        private struct LogState
        {
            public Uri Destination { get; set; }
            public List<string> LogEntries { get; set; }
        }

        private static Message CopyAndLogMessage(LogState state, MessageBuffer buffer)
        {
            var message = buffer.CreateMessage();
            var settings = new XmlWriterSettings();

            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineHandling = NewLineHandling.None;
            settings.Indent = false;

            using (var sw = new StringWriter())
            using (var xw = XmlWriter.Create(sw, settings))
            {
                message.WriteMessage(xw);
                xw.Flush();
                sw.Flush();
                state.LogEntries.Add(state.Destination.ToString() + "," + sw.ToString());
            }

            return buffer.CreateMessage();
        }

        private static void FlushLogEntries(List<string> logEntries)
        {
            File.AppendAllLines(Path.Combine(Path.GetTempPath(), "LogMessageWithBehaviour.txt"), logEntries);
        }

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var logState = new LogState { Destination = request.Headers.To, LogEntries = new List<string>() };
            request = CopyAndLogMessage(logState, request.CreateBufferedCopy(int.MaxValue));
            return logState;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            var logState = (LogState)correlationState;
            reply = CopyAndLogMessage(logState, reply.CreateBufferedCopy(int.MaxValue));
            FlushLogEntries(logState.LogEntries);
        }
    }
}