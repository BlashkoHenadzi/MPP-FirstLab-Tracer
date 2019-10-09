using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tracer.Tests
{
    [TestClass]
    public class TraceResultTests
    {
        [TestMethod]
        public void DeepClone_Object_CloneOfObject()
        {
            Object _testobject = new object();
            Assert.AreNotSame(TraceResult.DeepClone(_testobject), _testobject);
        }

    }
}
