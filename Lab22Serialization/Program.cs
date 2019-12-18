using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Collections.Generic;

namespace Lab22Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer(1, "Billy", "PA127892D");
            var customer2 = new Customer(2, "Phil", "DH087341C");
            var customer3 = new Customer(3, "Linda", "HG39432U");
            var customers = new List<Customer>() { customer, customer2 , customer3};

            //serialize customer to XML
            //create object for serialization
            var formatter = new SoapFormatter();
            //stream customer to file
            using (var stream = new FileStream("data.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, customers);
            }
            //print out file
            Console.WriteLine(File.ReadAllText("data.xml"));

            //Reverse
            //stream read
            var customersFromXML = new List<Customer>();
            using (var reader = File.OpenRead("data.xml"))
            {
                customersFromXML = formatter.Deserialize(reader) as List<Customer>;
            }
            //print

            customersFromXML.ForEach(c => Console.WriteLine($"{ c.CustomerId}{ c.CustomerName}"));
        }
    }
    [Serializable]
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        [NonSerialized]
        public string NINO;

        public Customer(int customerID, string customerName, string NINO)
        {
            this.CustomerId = customerID;
            this.CustomerName = customerName;
            this.NINO = NINO;
        }
    }
}
