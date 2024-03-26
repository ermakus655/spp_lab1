using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1.Tracer
{
    public class Tracer : ITracer
    {
        public List<ThreadList> threadList = new List<ThreadList>();
        public void startTrace()
        {
            var threadId = Environment.CurrentManagedThreadId;

            var stackTrace = new StackTrace();
            var callingMethodName = stackTrace.GetFrame(1).GetMethod().Name;
            var callingMethod = stackTrace.GetFrame(1).GetMethod();
            var callingClassName = callingMethod.DeclaringType.Name;
            var targetList = threadList.FirstOrDefault(t => t.threadId == threadId);
            Node newNode = new Node(callingMethodName, callingClassName);

            if (targetList == null)
            {
                ThreadList list = new ThreadList();
                list.funNodes.Add(newNode);
                list.threadId = threadId;
                threadList.Add(list);
            }
            else 
            {
                var targetNode = targetList.funNodes.FirstOrDefault(t => t.root().isRunning);
                if (targetNode == null)
                {
                    targetList.funNodes.Add(newNode);
                }
                else
                {
                    targetNode.addNode(newNode);
                    targetList.funNodes.Add(newNode);
                }
            }
            newNode.timer.Start();

        }
        public void stopTrace()
        {
            var threadId = Environment.CurrentManagedThreadId;
            var targetList = threadList.FirstOrDefault(t => t.threadId == threadId);
            int i = targetList.funNodes.Count - 1;
            while (!targetList.funNodes[i].isRunning) 
                i--;
            targetList.funNodes[i].isRunning = false;
            targetList.funNodes[i].timer.Stop();

        }
        public TraceResult getTraceResult()
        {
            var trace = new TraceResult();
            trace.getRootList(threadList);


            return trace;
        }
    }
}
