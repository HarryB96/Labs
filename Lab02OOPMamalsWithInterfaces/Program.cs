using System;

namespace Lab02OOPMamalsWithInterfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
    class Mamal
    {
        bool isWarmBlooded = true;
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
    }

    class Cat : Mamal, IUseVision, IUseSmell
    {
        public virtual string Roar()
        {
            return "Roar";
        }
        public virtual void SeeMyPrey()
        {

        }

        public virtual void SmellMyPrey()
        {
            
        }
    }
    class Lion : Cat
    {
        public override string Roar()
        {
            return "Lion is roaring";
        }
        public override void SeeMyPrey()
        {
            
        }
        public override void SmellMyPrey()
        {
            
        }
    }
    class Tiger : Cat 
    {
        public override string Roar()
        {
            return "Tiger is roaring";
        }
        public override void SeeMyPrey()
        {

        }
        public override void SmellMyPrey()
        {

        }
    }

    interface IUseVision
    {
        void SeeMyPrey();
    }
    interface IUseSmell
    {
        void SmellMyPrey();
    }
}
