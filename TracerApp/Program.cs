using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;
using System.Threading;
using System.IO;
using TracerApp.SerializationResult;
namespace TracerApp
{
    class Program
    {
        public class Foo
        {
            private Bar _bar;
            private Tracer.Tracer _tracer;

            internal Foo(Tracer.Tracer tracer)
            {
                _tracer = tracer;
                _bar = new Bar(_tracer);
            }

            public void MyMethod()
            {
                _tracer.StartTrace();
        
                _bar.InnerMethod();
                Thread.Sleep(300);

                _tracer.StopTrace();
            }
        }

        public class Bar
        {
            private Tracer.Tracer _tracer;

            internal Bar(Tracer.Tracer tracer)
            {
                _tracer = tracer;
            }

            public void InnerMethod()
            {
                _tracer.StartTrace();
                Console.Write("fffff");
                Thread.Sleep(100);
                _tracer.StopTrace();
            }
        }


        static private Tracer.Tracer _tracer = new Tracer.Tracer();
           

            static void DoIT()
            {
                _tracer.StartTrace();
                m0();
                _tracer.StopTrace();
                

            }

            static void m0()
            {
           // _tracer.StartTrace();
            Console.Write("testtttt");
           // _tracer.StopTrace();
        }

           

            static void Main(string[] args)
            {
                Tracer.Tracer _tracer = new Tracer.Tracer();
               Thread thread1 = new Thread(new Foo(_tracer).MyMethod);              
                thread1.Name = "FIRST";               
                thread1.Start();            
                Thread.Sleep(100);
               // thread2.Start();
                thread1.Join(); // wait all threads terminate
               // thread2.Join();
                foreach (TracedThread thread in _tracer.GetTraceResult().tracedthreadslist)
                Console.WriteLine(thread.threadId + "  " + thread.Time);
                Console.ReadLine();
            ITraceResult serial = new JsonSerializer();
                Console.WriteLine(serial.SerializeResult(_tracer.GetTraceResult()));
                Console.ReadLine();
        }
        
    }
}
