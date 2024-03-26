using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Tracer.serializer.ClassSerializer
{
    public class xml : ISerializer
    {
        public string serialize(TraceResult traceResult)
        {
            string result = "";
            result += "<root>\n";

            foreach (TThread thred in traceResult.rootList)
            {
                result += addThreads(thred);
            }

            result += "</root>";
            return result;
        }

        //PadRight(result.Length + 8); Добавление пробелов

        //Добавление потока
        public string addThreads(TThread trace)
        {
            string result = "";
            result = result.PadRight(result.Length + 4);
            result += $"<thread id=\"{trace.id}\" time=\"{trace.time}ms\">\n";
            foreach (Node method in trace.root)
                result += addMethod(method, 8);

            result = result.PadRight(result.Length + 4);
            result += "</thread>\n";

            return result;
        }

        //Добавление метода
        public string addMethod(Node method, int step)
        {
            string result = "";
            result = result.PadRight(result.Length + step);
            if (method.childs.Count != 0)
            {
                result += $"<method name=\"{method.methodName}\" time=\"{method.ResultTime()}ms\" class=\"{method.className}\">\n";

                foreach (Node child in method.childs)
                {
                    result += addMethod(child, step + 4);
                }

                result = result.PadRight(result.Length + step);
                result += "</method>\n";
            }
            else
            {
                result += $"<method name=\"{method.methodName}\" time=\"{method.ResultTime()}ms\" class=\"{method.className}\"/>\n";
            }
            return result;
        }
    }
}
