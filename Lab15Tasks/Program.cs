using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace Lab15Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            //create anonymous method or delegate using lambda
            var task01 = new Task(()=> { }); //lambda anonymous method
            var task02 = new Task(() => { Console.WriteLine("In Task 2"); });
            task02.Start();
            //Slicker
            var task03 = Task.Run(() => { Console.WriteLine("In task 3"); });
            var task04 = Task.Run(() => { Console.WriteLine("In task 4"); });
            var task05 = Task.Run(() => { Console.WriteLine("In task 5"); });
            //stopwatch
            //array of tasks
            //wait for one to complete / all to complete
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}
