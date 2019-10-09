using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections.Generic;
namespace Tracer.Tests
{
    [TestClass]
    public class TracerTests
    {
        private Tracer _tracer;
        private void Sleep250Method()
        {
            _tracer.StartTrace();
            Thread.Sleep(250);
            _tracer.StopTrace();
        }
        [TestInitialize]
        public void TestInitialize()
        {
            _tracer = new Tracer();
        }
        
        [TestMethod]
        public void Tracer_Sleep100_TimeGraterOrEqual100()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            Console.WriteLine(10);
            _tracer.StopTrace();
            long _expected = 100;
            Assert.IsTrue(_tracer.GetTraceResult().tracedthreadslist[0].Time >= _expected);
        }
        [TestMethod]
        public void Tracer_Sleep250InMultiThreadingTrace_EveryThreadTimeEqual250()
        {
            Random _rand = new Random();
            int _threadcount = _rand.Next(1, 40); 
            List<Thread> _threads= new List<Thread>();
            for(int i = 1; i <= _threadcount; i++)
            {
                _threads.Add(new Thread(Sleep250Method));
                _threads[i - 1].Start();
            }
            for (int i = 1; i <= _threadcount; i++)
            {                
                _threads[i - 1].Join();
            }
            long _expected = 250 * _threadcount;
            long _actual = 0;
            for (int i = 1; i <= _threadcount; i++)
            {
                _actual += _tracer.GetTraceResult().tracedthreadslist[i - 1].Time;
            }
            Assert.IsTrue(_actual >=_expected);
        }
    }
}
