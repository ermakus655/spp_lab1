﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Tracer
{
    //класс хранящий узлы потока и id потока
    public class ThreadList
    {
        public int threadId;
        public List <Node> funNodes = new List <Node> ();
    }
}
