using System;

namespace Lab10Events
{
    class Program
    {
        delegate void MyDelegate();
        delegate int MyDelegate2(int x);
        static event MyDelegate MyEvent;
        static event MyDelegate2 MyEvent2;
        static void Main(string[] args)
        {
            MyEvent += Method01;
            MyEvent += Method02;
            MyEvent2 += Method03;
            MyEvent();
            MyEvent2(10);
        }
        static void Method01()
        {
            Console.WriteLine("Running Method 01");
        }
        static void Method02()
        {
            Console.WriteLine("Running Method 02");
        }

        static int Method03(int x)
        {
            Console.WriteLine((int)Math.Pow(x, 2));
            return (int)Math.Pow(x, 2);
        }
    }
}
