﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tracer
{
    [Serializable]
    public class MethodTracing
    {
        public long Time;
        public  string methodname;
        public  string classname;
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
