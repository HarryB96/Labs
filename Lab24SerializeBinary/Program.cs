using System;
using Lab22Serialization;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab24SerializeBinary
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer(1, "Billy", "PA127892D");
            var customer2 = new Customer(2, "Phil", "DH087341C");
            var customer3 = new Customer(3, "Linda", "HG39432U");
            var customers = new List<Customer>() { customer, customer2, customer3 };

            //formatter
            var formatter = new BinaryFormatter();
            //stream to file
            using (var stream = new FileStream("data.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, customers);
            }
            //stream read
            var customersFromBinary = new List<Customer>();
            using (var reader = File.OpenRead("data.bin"))
            {
                customersFromBinary = formatter.Deserialize(reader) as List<Customer>;
            }
            //print
            customersFromBinary.ForEach(c => Console.WriteLine($"{c.CustomerId}{c.CustomerName}"));
        }
    }
}
