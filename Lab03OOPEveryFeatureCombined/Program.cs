using System;

namespace Lab03OOPEveryFeatureCombined
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    class Building
    {
        string location;
        string use;
        
        public int NumberOfFloors { get; set; }
        public bool Occupied { get; set; }
        public virtual void Build()
        {
            Console.WriteLine("A building was built");
        }
        public virtual void Build(int number)
        {
            Console.WriteLine("{0} buildings were built", number);
        }
    }
    sealed class Office : Building
    {
        public override void Build()
        {
            Console.WriteLine("An office was built");
        }
        public override void Build(int number)
        {
            Console.WriteLine("{0} offices were built", number);
        }
    }

    abstract class House : Building
    {
        public abstract void FurnishHouse();
    }
    class Home : House
    {
        public override void FurnishHouse()
        {
            Console.WriteLine("Full of nice things");
        }
    }
}
