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
        static private object locker = new object();
        public Tracer()
        {

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

        }
        private string GetClassNameByMethodName(MethodBase methodname)
        {
            return methodname.DeclaringType.ToString();
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
