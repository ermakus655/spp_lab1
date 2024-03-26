using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Lab1.LConsole
{
    public class Fun
    {
        public readonly Tracer.Tracer tracer = new Tracer.Tracer();
        public void Fun1()
        {
            tracer.startTrace();
            Thread.Sleep(100);
            Fun2();
            tracer.stopTrace();
        }

        public void Fun2()
        {
            tracer.startTrace();
            Thread.Sleep(200);
            tracer.stopTrace();
        }

        public void Fun3(int n)
        {
            tracer.startTrace();

            Thread.Sleep(50);
            if (n == 1)
                Fun1();

            if (n != 0)
                Fun3(--n);

            tracer.stopTrace();
        }
    }
}
