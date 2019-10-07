using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Tracer;
using System.Runtime.Serialization.Json;
namespace TracerApp.SerializationResult
{
    class JsonSerializer : ITraceResult
    {
        public string SerializeResult(TraceResult result)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(TraceResult));
            using (MemoryStream ms = new MemoryStream())
            {
                jsonFormatter.WriteObject(ms, result);
                var jsonString = Encoding.Default.GetString((ms.ToArray()));
                return jsonString;
            }

        }
    }
}
