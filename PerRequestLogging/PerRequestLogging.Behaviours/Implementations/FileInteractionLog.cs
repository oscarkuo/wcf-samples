using PerRequestLogging.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PerRequestLogging.Behaviours.Implementations
{
    // This class implements a simple File-based logging. This class _MUST_ be state-less because it is
    // up to Ninject to decide when a new instance is created. State information such as correlation ID and 
    // log buffer is stored in IInteractionState, which is designed to provide state information for the 
    // "interaction" session (i.e. process request and reply if applicatible)
    public class FileInteractionLog : IInteractionLog
    {
        private IInteractionState _state;

        public FileInteractionLog(IInteractionState state)
        {
            _state = state;
        }

        public void Flush()
        {
            var buf = _state.Get<List<string>>(Constants.InteractionLogBufferKey);
            File.AppendAllLines(Path.Combine(Path.GetTempPath(), "PerRequestlogging.csv"), buf);
            _state.Set(Constants.InteractionLogBufferKey, null);
        }

        private void Write(string type, string message, Exception exception)
        {
            if (_state == null)
            {
                throw new NullReferenceException("No interaction state provided");
            }

            var ex = "";
            if (exception != null)
            {
                ex = exception.ToString();
            }

            var uri = _state.Get<Uri>(Constants.ServiceRequestUriKey);
            var cid = _state.Get<Guid>(Constants.InternalCorrelationIdentifierKey);
            var buf = _state.Get<List<string>>(Constants.InteractionLogBufferKey);

            var builder = new StringBuilder();
            builder.AppendFormat("{0},{1},{2},{3},{4},{5},{6}",
                uri, 
                Thread.CurrentThread.ManagedThreadId, 
                cid,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), type, message, ex);
            buf.Add(builder.ToString());
        }

        public void WriteError(string message, Exception exception)
        {
            Write("Error", message, exception);
        }

        public void WriteWarning(string message)
        {
            Write("Warning", message, null);
        }

        public void WriteInformation(string message)
        {
            Write("Information", message, null);
        }

        public void WriteMessage(Message payload)
        {
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineHandling = NewLineHandling.None;
            settings.Indent = false;
            
            using (var sw = new StringWriter())
            using (var xw = XmlWriter.Create(sw, settings))
            {
                payload.WriteMessage(xw);
                xw.Flush();
                sw.Flush();
                Write("Message", sw.ToString(), null);
            }
        }
    }
}
