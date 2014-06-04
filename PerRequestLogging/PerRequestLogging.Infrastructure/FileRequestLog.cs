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

namespace PerRequestLogging.Infrastructure
{
    public class FileRequestLog : IRequestLog
    {
        public Uri ServiceEndpointUri { get; set; }
        public Guid InternalRequestIdentifier { get; set; }

        List<string> _buffer = new List<string>();

        public void Flush()
        {
            File.AppendAllLines(Path.Combine(Path.GetTempPath(), "wcfrequestslog.csv"), _buffer);
        }

        private void Write(string type, string message, Exception exception)
        {
            if (ServiceEndpointUri == null)
            {
                throw new NullReferenceException("ServiceEndpointUri");
            }
            if (InternalRequestIdentifier == null)
            {
                throw new NullReferenceException("InternalRequestIdentifier");
            }

            var ex = "";
            if (exception != null)
            {
                ex = exception.ToString();
            }                

            var builder = new StringBuilder();
            builder.AppendFormat("{0},{1},{2},{3},{4},{5},{6}", 
                ServiceEndpointUri.ToString(), Thread.CurrentThread.ManagedThreadId, 
                InternalRequestIdentifier.ToString(), 
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
