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
    public class Tracer:ITracer
    {      
        private ConcurrentDictionary<int, ConcurrentStack<MethodTracing>> threadtracinglist;
        private ConcurrentDictionary<int, ConcurrentStack<MethodTracing>> finishedthreads;
        static private object locker = new object();
        public Tracer()
        {
            threadtracinglist = new ConcurrentDictionary<int, ConcurrentStack<MethodTracing>>();
            finishedthreads = new ConcurrentDictionary<int, ConcurrentStack<MethodTracing>>();
        }
        public void StartTrace()
        {
            MethodBase MethodFrame = GetMethodFrame();
            int ThreadFrameId = Thread.CurrentThread.ManagedThreadId;           
            ConcurrentStack<MethodTracing> stack = threadtracinglist.GetOrAdd(ThreadFrameId, new ConcurrentStack<MethodTracing>());                       
            MethodTracing methodtracing = new MethodTracing(MethodFrame.Name, GetClassNameByMethodName(MethodFrame));                      
            stack.Push(methodtracing);
            methodtracing.StartCalculation();
        }
       
        public void StopTrace()
        {
            int ThreadFrameId = Thread.CurrentThread.ManagedThreadId;
            ConcurrentStack<MethodTracing> processingstack = null;
            ConcurrentStack<MethodTracing> finishedstack = finishedthreads.GetOrAdd(ThreadFrameId,new ConcurrentStack<MethodTracing>());
            threadtracinglist.TryGetValue(ThreadFrameId,out processingstack);
            MethodTracing method = null;
            if (processingstack != null)
            {
                processingstack.TryPop(out method);
                method.StopCalculation();
                if (processingstack.TryPeek(out var parentmethod))
                {
                    parentmethod.AddMethod(method);
                }
                else
                    finishedstack.Push(method);
            }
          


        }
        private string GetClassNameByMethodName(MethodBase methodname)
        {
            return methodname.DeclaringType.Name;
        }
        private MethodBase GetMethodFrame()
        {
            return new StackTrace().GetFrame(2).GetMethod();
        } 
        public TraceResult GetTraceResult()
        {
            return new TraceResult(finishedthreads);
        }
    }
    
}
