using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab09RabbitTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(RabbitCollection.MultiplyRabbits(8));
        }
    }
    public class RabbitCollection
    {
        public static List<Rabbit> rabbits;
        public static (int totalAge, int rabbitCount) MultiplyRabbits(int totalYears)
        {
            rabbits = new List<Rabbit>();
            int year = 1;
            int rabbitNumber = 1;
            var origin = new Rabbit(0, "Origin", 0);
            rabbits.Add(origin);
            while (year <= totalYears)
            {
                foreach (Rabbit r in rabbits.ToArray())
                {
                    if (r.RabbitAge >= 3)
                    {
                        var rabbit = new Rabbit(rabbitNumber, "Rabbit " + rabbitNumber, 0);
                        rabbits.Add(rabbit);
                        rabbitNumber++;
                    }

                    r.RabbitAge += 1;
                }
                year++;
            }
            int cumulativeAge = 0;
            rabbits.ForEach(r => cumulativeAge += r.RabbitAge);


            return (cumulativeAge, rabbitNumber);
        }
    }

    public class Rabbit
    {
        public Rabbit(int RabbitId, string RabbitName, int RabbitAge)
        {
            this.RabbitId = RabbitId;
            this.RabbitName = RabbitName;
            this.RabbitAge = RabbitAge;
        }
        public int RabbitId { get; set; }
        public string RabbitName { get; set; }
        public int RabbitAge { get; set; }
    }
}
