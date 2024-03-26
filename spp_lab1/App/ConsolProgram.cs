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

namespace Lab1.LConsole
{
    class Program
    {
        private static readonly Fun fun = new Fun();
        

        public static void Main()
        {
            var thread1 = new Thread(fun.Fun1);
            var thread2 = new Thread(fun.Fun2);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();


            fun.Fun3(4);
            fun.Fun2();
            var traceResult = fun.tracer.getTraceResult();

            Console.WriteLine("XML:");
            xml xmlSerializ = new xml();
            string messageXml = xmlSerializ.serialize(traceResult);
            Console.WriteLine(messageXml);
            File.WriteAllText("trace.xml", messageXml);
            Console.WriteLine();

            Console.WriteLine("JSON:");
            json jsonSerializ = new json();
            string messageJson = jsonSerializ.serialize(traceResult);
            Console.WriteLine(messageJson);
            File.WriteAllText("trace.json", messageJson);
        }
    }
}

