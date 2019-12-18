using System;

namespace Lab11Delegates
{
    class Program
    {
        public delegate void Delegate1();
        public delegate int Delegate2(int x);
        static void Main(string[] args)
        {
            var delegateInstance1 = new Delegate1(Method01);
            delegateInstance1();
            Delegate1 delegateInstance2 = Method01;
            delegateInstance2();
            Action delegateInstance3 = Method01;

            Delegate2 delegateInstance4 = (x) => { return ((int)Math.Pow(x, 3)); };
            Delegate2 delegateInstance5 = x => (int)Math.Pow(x, 3);
        }
        static void Method01()
        {
            Console.WriteLine("Running Method 01");
        }
        static void Method02()
        {
            Console.WriteLine("Running Method 02");
        }
    }

}
