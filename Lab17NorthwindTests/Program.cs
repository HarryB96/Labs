using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab17NorthwindTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var NorthCollection = new NorthwindCustomerCollection();
            Console.WriteLine(NorthCollection.peopleCount("London"));
            Console.WriteLine(NorthCollection.peopleCount(null));
            Console.ReadLine();
        }
    }
    public class NorthwindCustomerCollection
    {
        public int peopleCount (string city)
        {
            int total = 0;
            if (city == null || city == string.Empty)
            {
                using (var db = new NorthwindEntities())
                {
                    total = db.Customers.Count();
                }
            }
            else
            {
                using (var db = new NorthwindEntities())
                {
                    total = db.Customers.Where(c => c.City == city).Count();
                }
            }
            return total;
        }
    }
}
