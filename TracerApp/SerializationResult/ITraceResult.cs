using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;
using System.IO;
namespace TracerApp.SerializationResult
{
    public interface ITraceResult
    {
        string SerializeResult(TraceResult result);
    }
}
