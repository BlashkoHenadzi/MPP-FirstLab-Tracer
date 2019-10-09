using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections.Generic;
namespace Tracer.Tests
{
    [TestClass]
    public class TracerTests
    {
        static Tracer _tracer;
        class Foo
        {
            public Foo()
            {
                _tracer.StartTrace();
                _tracer.StopTrace();

            }

        }
        class Bar
        {
            public Bar()
            {
                _tracer.StartTrace();
                Thread.Sleep(10);
                Foo _foo = new Foo();
                _tracer.StopTrace();

            }
        }
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
        public void Tracer_ChildClassMethodTesting_OneChildMethod()
        {
            Bar _bar = new Bar();
            Assert.AreEqual(_tracer.GetTraceResult().tracedthreadslist[0].threadmethodslist.Count, 1);
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
        [TestMethod]
        public void Tracer_OneMethodExec_MethodCountIsOne()
        {
            Foo _foo = new Foo();
            int _expected = 1;
            Assert.AreEqual(_tracer.GetTraceResult().tracedthreadslist.Count, _expected);
        }
        [TestMethod]
        public void Tracer_OneMethodExec_ChildMethodCountIsZero()
        {
            Foo _foo = new Foo();
            int _expected = 1;
            int _actual = _tracer.GetTraceResult().tracedthreadslist[0].threadmethodslist.Count;
            Assert.AreEqual(_expected , _actual);
        }
        [TestMethod]
        public void Tracer_FooClassMethodExec_FooNameClass()
        {
            Foo _foo = new Foo();
            string _expected = "Foo";
            string _acteul = _tracer.GetTraceResult().tracedthreadslist[0].threadmethodslist[0].classname;
            Assert.AreEqual(_expected, _acteul );
        }
        [TestMethod]
        public void Tracer_FooClassMethodExec_FooClassMethodName()
        {
            Foo _foo = new Foo();
            string _expected = "Foo";
            string _actual = _tracer.GetTraceResult().tracedthreadslist[0].threadmethodslist[0].methodname;
            Assert.AreEqual(_expected, _actual);
        }
    }
}
