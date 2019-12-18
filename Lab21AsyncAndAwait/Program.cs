using System;
using System.IO;
using System.Threading;
using System.Net;
using System.Diagnostics;

namespace Lab21AsyncAndAwait
{
    class Program
    {
        static Uri uri = new Uri("https://www.bbc.co.uk/weather");
        
        static void Main(string[] args)
        {
            //Main method
            //setup create data file
            File.WriteAllText("data.csv", "id,name,age");
            File.AppendAllText("data.csv", "\n1,Bob,21");
            File.AppendAllText("data.csv", "\n2,Tina,23");
            File.AppendAllText("data.csv", "\n3,Paul,24");
            //sync method - wait for it
            //ReadDataSync();
            //async method - don't wait for it
            //ReadDataAsync();
            Stopwatch s = new Stopwatch();
            s.Start();
            GetWebPageSync();
            Console.WriteLine($"Sync finished at { s.ElapsedMilliseconds} ms");
            GetWebPageAsync();
            Console.WriteLine($"Async finished at {s.ElapsedMilliseconds} ms");
            Console.WriteLine($"Code has finished {s.ElapsedMilliseconds} ms");

        }

        static void ReadDataSync()
        {
            var output = File.ReadAllText("data.csv");
            Thread.Sleep(5000);
            Console.WriteLine("\nSync\n");
            Console.WriteLine(output);
        }
        async static void ReadDataAsync()
        {
            var output = await File.ReadAllTextAsync("data.csv");
            Thread.Sleep(5000);
            Console.WriteLine("\nAsync\n");
            Console.WriteLine(output);
        }
        
        static void GetWebPageSync()
        {
            
            Console.WriteLine($"Host is {uri.Host}, port is {uri.Port}, path is {uri.AbsolutePath}");

            var webClient = new WebClient { Proxy = null };
            var output = webClient.DownloadString(uri);
            //Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "localPage.html");
        }
        async static void GetWebPageAsync()
        {        
            Console.WriteLine($"Host is {uri.Host}, port is {uri.Port}, path is {uri.AbsolutePath}");

            var webClient = new WebClient { Proxy = null };
            var output = await webClient.DownloadStringTaskAsync(uri);
            
            //Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "localPage.html");
        }
    }
}
