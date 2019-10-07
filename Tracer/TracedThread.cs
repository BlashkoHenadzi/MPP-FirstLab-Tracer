using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TracedThread
    {
        public List<MethodTracing> threadmethodslist;
        public long threadtime;
        public int threadId;
        public TracedThread(int threadId, List<MethodTracing> threadmethodslist)
        {
            this.threadId = threadId;
            this.threadmethodslist = threadmethodslist;
            this.threadtime = 0;
            foreach (MethodTracing method in threadmethodslist)
                threadtime += method.Time;

        }
    }
}
