using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Tracer;
using System.Xml.Serialization;
namespace TracerApp.SerializationResult
{
    class XMLSerializer : ITraceResult
    {
        public string SerializeResult( TraceResult result)
        {            
            XmlSerializer XmlFormatter = new XmlSerializer(typeof(TraceResult));
            using (MemoryStream ms = new MemoryStream())
            {
                XmlFormatter.Serialize (ms, result);
                var XMLString = Encoding.Default.GetString((ms.ToArray()));
                return XMLString;
            }
        }
    }
}
