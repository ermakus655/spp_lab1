using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1.Tracer
{
    public class TThread
    {
        public List<Node> root = new List<Node>();
        public int id { get; }
        public long time { get; set; }

        public TThread(int id)
        {
            this.id = id;
        }

        public void getTrheadTime()
        {
            long Ttime = 0;
            foreach (Node node in root)
            {
                 Ttime += node.ResultTime();
            }
            time = Ttime;
        }
    }

    public class TraceResult
    {
        public List<TThread> rootList = new List<TThread>();
        public void getRootList(List<ThreadList> nodeList)
        {
            foreach (ThreadList list in nodeList)
            {
                foreach(Node node in list.funNodes) 
                {
                    if (node.parent == null) 
                    {
                        var targetThread = rootList.FirstOrDefault(t => t.id == list.threadId);
                        if (targetThread != null)
                        {
                            targetThread.root.Add(node);
                        }
                        else
                        {
                            TThread newTrace = new TThread(list.threadId);
                            newTrace.root.Add(node);
                            rootList.Add(newTrace);

                        }
                    }
                }
            }


            foreach (TThread node in rootList)
            {
                node.getTrheadTime();
            }
        } 
    }
}
