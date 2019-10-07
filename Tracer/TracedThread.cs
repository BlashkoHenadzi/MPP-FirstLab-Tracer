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
        [DataMember]
        public List<MethodTracing> threadmethodslist;
        [DataMember]
        public long threadtime;
        [DataMember]
        public int threadId;
        public TracedThread(int threadId, List<MethodTracing> threadmethodslist)
        {
            this.threadId = threadId;
            this.threadmethodslist = threadmethodslist;
            this.threadtime = 0;
            foreach (MethodTracing method in threadmethodslist)
                threadtime += method.Time;

        }
        public TracedThread()
        {
        }
    }
}
