using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static Stopwatch s = new Stopwatch();
        static void Main(string[] args)
        {
            s.Start();
            var task01 = Task.Run(() => 
            {
                Console.WriteLine("Task01 is running");
                Console.WriteLine($"Task01 has completed in {s.Elapsed}"); 
            });
            var actionDelegate = new Action(SpecialActionMethod);
            var task02 = new Task(actionDelegate);
            task02.Start();

            //Array of anonymous tasks
            Task[] taskArray = new Task[]
            {
                new Task(()=>{ }),
                new Task(()=>{ }),
                new Task(()=>{ }),
                new Task(()=>{ }),
                new Task(()=>{ }),
                new Task(()=>{ }),
            };
            foreach (var task in taskArray)
            {
                task.Start();
            }
            
            var taskArray2 = new Task[3];
            taskArray2[0] = Task.Run(() => 
            {
                Thread.Sleep(500);
                Console.WriteLine($"Task1 took {s.Elapsed}"); 
            });
            taskArray2[1] = Task.Run(() => 
            {
                Thread.Sleep(200);
                Console.WriteLine($"Task2 took {s.Elapsed}"); 
            });
            taskArray2[2] = Task.Run(() => 
            {
                Thread.Sleep(100);
                Console.WriteLine($"Task3 took {s.Elapsed}"); 
            });

            //Task.WaitAny(taskArray2);
            //Task.WaitAll(taskArray2);

            //parallel foreach loop
            var AsyncS = new Stopwatch();
            AsyncS.Start();
            int[] myCollection = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            Parallel.ForEach(myCollection, (item) =>
             {
                 Thread.Sleep(item * 100);
                 Console.WriteLine($"For each item {item} finishing at {AsyncS.Elapsed}");
             });
            AsyncS.Stop();
            //var SyncS = new Stopwatch();
            //SyncS.Start();
            //foreach (var item in myCollection)
            //{
            //    Thread.Sleep(item * 100);
            //    Console.WriteLine($"Sync For each item {item} finishing at {SyncS.Elapsed}");
            //}
            //SyncS.Stop();

            //Parallel linq
            var databaseOutput =
                (from item in myCollection
                 select Math.Pow(item, 2)).AsParallel().ToList();
            databaseOutput.ForEach(num => Console.WriteLine(num));

            //Getting data back from tasks
            var TaskWithoutReturningData = new Task(() => { });
            var TaskWithReturningData = new Task<int>(() => 
            {
                int total = 0;
                for (int i = 0; i < 100; i++)
                {
                    total += i;
                }
                return total;
            });
            TaskWithReturningData.Start();
            Console.WriteLine($"Counted to 100 in background sum is {TaskWithReturningData.Result}, act of waiting for result turns into a synchronous operation");

            Console.WriteLine($"Main method: all tasks complete at time {s.Elapsed}");
            Console.ReadLine();
        }
        static void SpecialActionMethod()
        {
            Console.WriteLine("This action method takes no parameters, returns nothing, just performs and action");
            long total =0;
            for (int i=0;i<1_000_000_000; i++)
            {
                total += i;
            }
            Console.WriteLine(total);
            Console.WriteLine($"Special action method completing at {s.Elapsed}");
        }
    }
}
