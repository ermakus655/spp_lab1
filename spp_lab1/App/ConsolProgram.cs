using System;
using Lab1.Tracer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using Lab1.Tracer.serializer.ClassSerializer;
using System.Collections.Concurrent;

namespace Lab1.LConsole
{
    class Program
    {
        private static readonly Fun fun = new Fun();
        public static ConcurrentDictionary<int,Thread> funThread = new ConcurrentDictionary<int,Thread>();
        

        public static void Main()
        {
            var thread1 = new Thread(fun.Fun1);
            var thread2 = new Thread(fun.Fun2);

            funThread.TryAdd(0, thread1);
            funThread.TryAdd(1, thread2);
            funThread[0].Start();
            funThread[1].Start();
            funThread[0].Join();
            funThread[1].Join();


            fun.Fun3(4);
            fun.Fun2();
            var traceResult = fun.tracer.getTraceResult();

            Console.WriteLine("XML:");
            xml xmlSer = new xml();
            string XmlFile = xmlSer.serialize(traceResult);
            Console.WriteLine(XmlFile);
            File.WriteAllText("trace.xml", XmlFile);
            Console.WriteLine();

            Console.WriteLine("JSON:");
            json jsonSer = new json();
            string JsonFile = jsonSer.serialize(traceResult);
            Console.WriteLine(JsonFile);
            File.WriteAllText("trace.json", JsonFile);
        }
    }
}

