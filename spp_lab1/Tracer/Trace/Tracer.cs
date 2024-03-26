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
        //начало отслеживания
        public void startTrace()
        {
            var threadId = Environment.CurrentManagedThreadId;
            //получение имени метода и класса
            var stackTrace = new StackTrace();
            var callingMethodName = stackTrace.GetFrame(1).GetMethod().Name;
            var callingMethod = stackTrace.GetFrame(1).GetMethod();
            var callingClassName = callingMethod.DeclaringType.Name;
            var targetList = threadList.FirstOrDefault(t => t.threadId == threadId);
            Node newNode = new Node(callingMethodName, callingClassName);

            if (targetList == null)
            {
                // создание нового узла потока со списком узлов и id
                ThreadList list = new ThreadList();
                list.funNodes.Add(newNode);
                list.threadId = threadId;
                threadList.Add(list);
            }
            else 
            {
                //добаваление нового узла
                var targetNode = targetList.funNodes.FirstOrDefault(t => t.root().isRunning);
                if (targetNode == null)
                {
                    //добаление нового корня
                    targetList.funNodes.Add(newNode);
                }
                else
                {
                    //добавление дочернего узла вниз
                    targetNode.addNode(newNode);
                    targetList.funNodes.Add(newNode);
                }
            }
            //старт таймера
            newNode.timer.Start();

        }
        //окончание отслеживания
        public void stopTrace()
        {
            var threadId = Environment.CurrentManagedThreadId;
            var targetList = threadList.FirstOrDefault(t => t.threadId == threadId);
            int i = targetList.funNodes.Count - 1;
            //поиск последнего работающего узла
            while (!targetList.funNodes[i].isRunning) 
                i--;
            targetList.funNodes[i].isRunning = false;

            //остановка таймера
            targetList.funNodes[i].timer.Stop();

        }
        //получение результатов отслеживания
        public TraceResult getTraceResult()
        {
            var trace = new TraceResult();
            trace.getRootList(threadList);
            return trace;
        }
    }
}
