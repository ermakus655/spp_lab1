using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Tracer.serializer.ClassSerializer
{
    public class json : ISerializer
    {
        public string serialize(TraceResult traceResult)
        {
            string result = "";
            result += "{\n    \"threads\": [\n";

            foreach (TThread thred in traceResult.rootList)
            {
                result = result.PadRight(result.Length + 8);
                result += "{\n";

                result += addThreads(thred);

                result = result.PadRight(result.Length + 8);
                result += "},\n"; 
            }
            result = result.TrimEnd(',', '\n') + "\n";
            result += "    ]\n}";
            return result;
        }

        public string addThreads(TThread trace)
        {
            string result = "";

            result = result.PadRight(result.Length + 12);
            result += $"\"id\": \"{trace.id}\",\n";

            result = result.PadRight(result.Length + 12);
            result += $"\"time\": \"{trace.time}\",\n";

            result = result.PadRight(result.Length + 12);
            result += $"\"methods\": [\n";

            foreach (Node method in trace.root)
            {
                result = result.PadRight(result.Length + 16);
                result += "{\n";

                result += addMethod(method, 20);

                result = result.PadRight(result.Length + 16);
                result += "},\n";
            }
            result = result.TrimEnd(',', '\n') + "\n";
            result = result.PadRight(result.Length + 12);
            result += $"]\n";

            return result;
        }

        public string addMethod(Node method, int step)
        {
            string result = "";

            result = result.PadRight(result.Length + step);
            result += $"\"name\": \"{method.methodName}\",\n";

            result = result.PadRight(result.Length + step);
            result += $"\"class\": \"{method.className}\",\n";

            result = result.PadRight(result.Length + step);
            result += $"\"time\": \"{method.ResultTime()}\",\n";

            result = result.PadRight(result.Length + step);
            result += $"\"methods\": [\n";

            foreach (Node child in method.childs)
            {
                result = result.PadRight(result.Length + step + 4);
                result += "{\n";

                result += addMethod(child, step + 8);

                result = result.PadRight(result.Length + step + 4);
                result += "},\n";
            }
            result = result.TrimEnd(',', '\n') + "\n";
            result = result.PadRight(result.Length + step);
            result += "]\n";
            
            return result;
        }
    }
}
