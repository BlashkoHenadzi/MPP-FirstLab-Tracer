using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tracer
{
    class MethodTracing
    {
        private Stopwatch Watch { get; }
        private readonly string methodname;
        private readonly string classname;
        private readonly List<MethodTracing> MethodsList;
        private  long Time;
        public  MethodTracing(string methodname, string classname)
        {
            this.classname = classname;
            this.methodname = methodname;
            MethodsList = new List<MethodTracing>();
            Watch = new Stopwatch();
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
