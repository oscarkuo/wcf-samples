using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Channels;
using System.Xml;

namespace LogMessageWithBehaviour.Services
{
    public class Log
    {
        private Uri _uri;
        private string _path;
        private List<string> _entries;

        public Log(string path, Uri uri)
        {
            _uri = uri;
            _path = path;
            _entries = new List<string>();
        }

        public void Write(Message message)
        {
            var settings = new XmlWriterSettings();

            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineHandling = NewLineHandling.None; // strip new line characters
            settings.Indent = false;

            using (var sw = new StringWriter())
            using (var xw = XmlWriter.Create(sw, settings))
            {
                message.WriteMessage(xw);
                xw.Flush();
                sw.Flush();
                _entries.Add(_uri + "," + sw.ToString());
            }
        }

        public void Flush()
        {
            File.AppendAllLines(_path, _entries);
        }
    }
}