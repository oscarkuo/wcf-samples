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
    public class FileInteractionLog : IInteractionLog
    {
        private List<string> _buffer = new List<string>();
        private IInteractionState _state;

        public FileInteractionLog(IInteractionState state)
        {
            _state = state;
        }

        public void Flush()
        {
            File.AppendAllLines(Path.Combine(Path.GetTempPath(), "wcfrequestslog.csv"), _buffer);
        }

        private void Write(string type, string message, Exception exception)
        {
            var ex = "";
            if (exception != null)
            {
                ex = exception.ToString();
            }

            var uri = "(null state)";
            var cid = "(null state)";

            if (_state != null)
            {
                uri = _state.Get<string>("ServiceRequestUri") ?? uri;
                cid = _state.Get<string>("InternalCorrelationIdentifier") ?? cid;
            }

            var builder = new StringBuilder();
            builder.AppendFormat("{0},{1},{2},{3},{4},{5},{6}",
                uri, 
                Thread.CurrentThread.ManagedThreadId, 
                cid,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), type, message, ex);
            _buffer.Add(builder.ToString());
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
