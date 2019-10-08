using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Serialization;
namespace Tracer
{
    [DataContract]
    [Serializable]
    public class MethodTracing
    {
        [DataMember(Order = 2)]
        public long Time;
        [DataMember (Order = 0)]
        public  string methodname;
        [DataMember(Order = 1)]
        public  string classname;
        [DataMember(Order = 3)]
        public  List<MethodTracing> MethodsList;
        
        [NonSerialized]
        private Stopwatch Watch;
        public  MethodTracing(string methodname, string classname)
        {
            this.classname = classname;
            this.methodname = methodname;
            MethodsList = new List<MethodTracing>();
            Watch = new Stopwatch();
        }
        public MethodTracing()
        {

        }

        public void StartCalculation()
        {
            Watch.Start();
        }
        public void StopCalculation()
        {
            Watch.Stop();
            this.Time = Watch.ElapsedMilliseconds;
        }
        public void AddMethod(MethodTracing method)
        {
           
            this.MethodsList.Add(method);
            
        }
    }
}
