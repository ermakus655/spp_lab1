using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;
using Lab1.Tracer;
using Lab1.LConsole;
using System.Threading;
using System.Collections.Generic;



namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        Fun fun = new Fun();
        
        [TestMethod]
        [TestInitialize]
        public void Initialize()
        {
            fun.Fun1();
            fun.Fun2();
            var thread1 = new Thread(fun.Fun2);
            thread1.Start();
            thread1.Join();
        }
        [TestMethod]
        public void TestGetRootList()
        {
            TraceResult result = new TraceResult();
            result.getRootList(fun.tracer.threadList);
            result.rootList.Count.Should().Be(2);
            result.rootList[0].getTrheadTime();
            result.rootList[0].time.Should().BeInRange(500, 600);
        }
        [TestMethod]
        public void TestGetTime()
        {
            TraceResult result = new TraceResult();
            result.getRootList(fun.tracer.threadList);

            result.rootList[0].getTrheadTime();
            result.rootList[0].time.Should().BeInRange(500, 600);

            result.rootList[1].getTrheadTime();
            result.rootList[1].time.Should().BeInRange(200, 300);
        }

        [TestMethod]
        public void TestFun()
        {
            Fun Fun1 = new Fun();
            Fun1.Fun1();
            Fun1.tracer.threadList[0].funNodes.Count.Should().Be(2);

            Fun Fun2 = new Fun();
            Fun2.Fun2();
            Fun2.tracer.threadList[0].funNodes.Count.Should().Be(1);

            Fun Fun3 = new Fun();
            Fun3.Fun3(4);
            Fun3.tracer.threadList[0].funNodes.Count.Should().Be(7);
        }
        [TestMethod]
        public void TestRootNode()
        {
            Node TestMethod1 = new Node("MethodName1", "ClassName1");
            Node TestMethod2 = new Node("MethodName2", "ClassName2");
            TestMethod1.addNode(TestMethod2);
            Node tempMethod = TestMethod2.root();
            TestMethod1.childs.Count.Should().Be(1);
            TestMethod2.parent.Should().Be(TestMethod1);
            tempMethod.Should().Be(TestMethod1);
        }

    }
}
