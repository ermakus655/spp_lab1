using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Tracer
{
    public class Node
    {
        public Node parent = null;
        public List<Node> childs = new List<Node>();
        public string methodName { get; set; }
        public string className { get; set; }
        public Stopwatch timer = new Stopwatch();
        public bool isRunning = true;
        public Node(string MethodName, string ClassName)
        {
            methodName = MethodName;
            className = ClassName;
        }
        public void addNode(Node newNode)
        {
            Node curNode = this;
            bool flag = false;
            while (!flag)
            {
                flag = true;
                foreach (Node child in curNode.childs)
                {
                    if (child.isRunning)
                    {
                        flag = false;
                        curNode = child;
                        break;
                    }
                }
            }
            curNode.childs.Add(newNode);
            newNode.parent = curNode;
        }

        public Node root()
        {
            Node curNode = this;
            while(curNode.parent != null)
            {
                curNode = curNode.parent;
            }
            return curNode;
        }
        public long ResultTime()
        {
            return timer.ElapsedMilliseconds;
        }
    }
}
