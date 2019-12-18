using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Lab06Rabbits100
{
    class Program
    {
        public static List<Rabbit> RabbitList = new List<Rabbit>();

        static void Main(string[] args)
        {
            //var rabbit = new Rabbit()
            //{
            //    RabbitName = "The General",
            //    RabbitAge = 10
            //};

            int rabbitNumber = 1;

            var rabbit = new Rabbit(rabbitNumber, "Rabbit " + rabbitNumber, 0);
            addRabbit(rabbit);
            rabbitNumber++;

            while (rabbitNumber < 1000)
            {
                using (var db = new RabbitDbContext())
                {
                    RabbitList = db.Rabbits.ToList();
                }

                //Updating age by 1
                foreach (Rabbit r in RabbitList)
                {
                    updateAge(r);
                }

                //Every Rabbit makes another
                foreach (Rabbit r in RabbitList)
                {
                    if (r.RabbitAge > 3)
                    {
                        var rabbitBaby = new Rabbit(rabbitNumber, "Rabbit " + rabbitNumber, 0);
                        addRabbit(rabbitBaby);
                        rabbitNumber++;
                    }
                    if (r.RabbitAge >= 12)
                    {
                        deleteRabbit(r);
                    }
                }
            }
            printRabbits();
        }



        static void printRabbits()
        {
            using (var db = new RabbitDbContext())
            {
                RabbitList = db.Rabbits.ToList();
            }

            foreach (var rabbit in RabbitList)
            {
                Console.WriteLine($"{rabbit.RabbitID, 3}{rabbit.RabbitName, 15}{rabbit.RabbitAge, 3}");
            }
        }
        static void addRabbit(Rabbit r)
        {
            using (var db = new RabbitDbContext())
            {
                db.Rabbits.Add(r);
                db.SaveChanges();
            }  
        }
        static void updateRabbit(Rabbit r)
        {
            using (var db = new RabbitDbContext())
            {
                r.RabbitAge = 2;
                db.Rabbits.Update(r);
                db.SaveChanges();
            }
        }
        static void updateAge(Rabbit r)
        {
            using (var db = new RabbitDbContext())
            {
                r.RabbitAge++;
                db.Rabbits.Update(r);
                db.SaveChanges();
            }
        }
        static void deleteRabbit(Rabbit r)
        {
            using (var db = new RabbitDbContext())
            {
                db.Rabbits.Remove(r);
                db.SaveChanges();
            }
        }
    }
    class Rabbit 
    {
        public Rabbit()
        {

        }
        public Rabbit(int RabbitID, string RabbitName, int RabbitAge)
        {
            this.RabbitID = RabbitID;
            this.RabbitName = RabbitName;
            this.RabbitAge = RabbitAge;
        }
        public int RabbitID { get; set; }
        public string RabbitName { get; set; }
        public int RabbitAge { get; set; }
    }
    class RabbitDbContext : DbContext
    {
        public DbSet<Rabbit> Rabbits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RabbitDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            builder.UseSqlServer(connectionString);
        }
    }
}
