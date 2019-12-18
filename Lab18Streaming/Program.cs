using System;
using System.IO; //Input Output
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace Lab18Streaming
{
    class Program
    {
        static void Main(string[] args)
        {
            //system.io writing files
            File.WriteAllText("data.txt", "Hello this is some data");

            var myArray = new string[] { "Hello", "this", "is", "some", "data" };
            File.WriteAllLines("ArrayData.txt", myArray);

            Console.WriteLine(File.ReadAllText("data.txt"));

            for (int i = 0; i < 10; i++)
            {
                File.AppendAllText("data.txt", Environment.NewLine + $"Adding line {i} at {DateTime.Now}");
            }

            var output = File.ReadAllLines("ArrayData.txt").ToList();
            output.ForEach(line => Console.WriteLine(line));

            //Stream data to file
            var numberOfLines = 10_000;
            var s = new Stopwatch();

            using (var stream01 = new StreamWriter("output.dat"))
            {
                for (int i = 0; i < numberOfLines; i++)
                {
                    stream01.WriteLine($"Logging some data at {DateTime.Now}");
                }
            }

            var writeTime = s.ElapsedMilliseconds;
            string nextLine;

            using (var reader = new StreamReader("output.dat"))
            {
                //reader does not know how big the file is
                //read until reader.readline is null
                while ((nextLine = reader.ReadLine()) != null)
                {
                    //Console.WriteLine(nextLine);
                }
                reader.Close();
            }

            Console.WriteLine($"It took {s.ElapsedMilliseconds - writeTime} to read {numberOfLines} lines");

            s.Restart();
            var longString = string.Empty;
            using (var reader = new StreamReader("output.dat"))
            {
                while ((nextLine = reader.ReadLine()) != null)
                {
                    longString += nextLine;
                }
                reader.Close();
            }
            Console.WriteLine($"It took {s.ElapsedMilliseconds} to add {numberOfLines} strings together");

            s.Restart();
            longString = string.Empty;
            var stringBuilder = new StringBuilder();
            using (var reader = new StreamReader("output.dat"))
            {
                while ((nextLine = reader.ReadLine()) != null)
                {
                    stringBuilder.Append(nextLine);
                }
                reader.Close();
            }
            Console.WriteLine($"It took {s.ElapsedMilliseconds} to add {numberOfLines} strings together using string builder");
        }
    }
}
