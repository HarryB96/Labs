using System;

namespace Lab12OOPEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            var James = new Child("James");
            James.Grow();
        }
    }
    class Child
    {
        public Child(string Name)
        {
            this.Name = Name;
            this.Age = 0;
            HaveABirthday += HaveAParty;
        }
        delegate void BirthdayDelegate();
        event BirthdayDelegate HaveABirthday;
        public string Name { get; set; }
        public int Age { get; set; }
        public  void HaveAParty()
        {
            this.Age++;
            Console.WriteLine("Celebrating another year " + $"Age is now {this.Age}");
            
        }
        public void Grow()
        {
            HaveABirthday();
        }
    }
}
