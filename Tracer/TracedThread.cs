using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Tracer
{
    [DataContract]
    [Serializable]
    public class TracedThread
    {
        [DataMember(Order = 2)]
        public List<MethodTracing> threadmethodslist;
        [DataMember (Order = 0)]
        public int threadId;
        [DataMember(Order = 1)]
        public long Time;      
       
        public TracedThread(int threadId, List<MethodTracing> threadmethodslist)
        {
            this.threadId = threadId;
            this.Time = 0;
            foreach (MethodTracing method in threadmethodslist)
                this.Time += method.Time;
            this.threadmethodslist = threadmethodslist;
            
           

        }
        public TracedThread()
        {
        }
    }
}
