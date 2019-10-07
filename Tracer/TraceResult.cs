using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Tracer
{
    public class TraceResult
    {
        public List<TracedThread> tracedthreadslist;        
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

         public  TraceResult(ConcurrentDictionary<int, ConcurrentStack<MethodTracing>> threads)
        {
            tracedthreadslist = new List<TracedThread>();            
            foreach (KeyValuePair<int,ConcurrentStack<MethodTracing>> thread in threads)
            {
                List<MethodTracing> clonedmethods = DeepClone(thread.Value).ToList();
                tracedthreadslist.Add(new TracedThread(thread.Key, clonedmethods));
            }          
            
        }           
        
    }
}
