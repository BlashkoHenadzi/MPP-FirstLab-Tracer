using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading;
namespace Tracer
{
    class Tracer:ITracer
    {
        private TraceResult FinalTraceResult;
        private ConcurrentDictionary<int, ConcurrentStack<MethodTracing>> threadtracinglist;
        ConcurrentStack<MethodTracing> finishedstack;
        static private object locker = new object();
        public Tracer()
        {

        }
        public void StartTrace()
        {
            MethodBase MethodFrame = GetMethodFrame();
            int ThreadFrameId = Thread.CurrentThread.ManagedThreadId;           
            ConcurrentStack<MethodTracing> stack = threadtracinglist.GetOrAdd(ThreadFrameId, new ConcurrentStack<MethodTracing>());
            

            MethodTracing parentmethod;
            stack.TryPeek(out parentmethod);
            MethodTracing methodtracing = new MethodTracing(MethodFrame.Name, GetClassNameByMethodName(MethodFrame));
            if (parentmethod != null)
            {
                parentmethod.AddMethod(methodtracing);
                
            }            
            stack.Push(methodtracing);
            methodtracing.StartCalculation();
        }
       
        public void StopTrace()
        {
            int ThreadFrameId = Thread.CurrentThread.ManagedThreadId;
            ConcurrentStack<MethodTracing> stack = null; ;
            threadtracinglist.TryGetValue(ThreadFrameId,out stack);
            MethodTracing method = null;
            if (stack != null)
            {
                stack.TryPop(out method);
                method.StopCalculation();
                if (stack.TryPop(out var parentmethod))
                {
                    parentmethod.AddMethod(method);
                }
                else
                    finishedstack.Push(method);
            }
          


        }
        private string GetClassNameByMethodName(MethodBase methodname)
        {
            return methodname.DeclaringType.ToString();
        }
        private MethodBase GetMethodFrame()
        {
            return new StackTrace().GetFrame(1).GetMethod();
        } 
        public TraceResult GetTraceResult()
        {
            return null;
        }
    }
    
}
