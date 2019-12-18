using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Lab23SerializeJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer(1, "Billy", "PA127892D");
            var customer2 = new Customer(2, "Phil", "DH087341C");
            var customer3 = new Customer(3, "Linda", "HG39432U");
            var customers = new List<Customer>() { customer, customer2, customer3 };

            //serialize
            var JSONCustomerList = JsonConvert.SerializeObject(customers);
            Console.WriteLine(JSONCustomerList);
            //save to file
            File.WriteAllText("data.json", JSONCustomerList);
            //read
            var JSONString = File.ReadAllText("data.json");
            var customersFromJSON = JsonConvert.DeserializeObject<List<Customer>>(JSONString);
            //print

            customersFromJSON.ForEach(c => Console.WriteLine($"{c.CustomerId}{c.CustomerName}"));
        }
    }

    [Serializable]
    class Customer
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
