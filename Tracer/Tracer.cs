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
        private ConcurrentDictionary<int, ThreadTracing> threadtracinglist;
        static private object locker = new object();
        public Tracer()
        {

        }
        public void StartTrace()
        {
            MethodBase MethodFrame = GetMethodFrame();
            int ThreadFrameId = Thread.CurrentThread.ManagedThreadId;
            MethodTracing methodtracing = new MethodTracing();
            ThreadTracing threadtracing = AddToThreadTracingList(ThreadFrameId);
        }
        public void StopTrace()
        {

        }
        private MethodBase GetMethodFrame()
        {
            return new StackTrace().GetFrame(1).GetMethod();
        } 
        private ThreadTracing AddToThreadTracingList(int ThreadId)
        {
            lock (locker)
            {
                ThreadTracing threadtracing;
                if (!threadtracinglist.TryGetValue(ThreadId,out threadtracing))
                {
                    threadtracing = new ThreadTracing(/*ThreadId*/); //TODO:Write constructor for ThreadTracing
                    threadtracinglist.TryAdd(ThreadId, threadtracing);
                }
                return threadtracing;
            }
            
        }

        public TraceResult GetTraceResult()
        {
            return null;
        }
    }
    
}
