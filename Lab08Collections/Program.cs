using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab08Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ArrayListDictionary.GetTotal(8, 5, 11, 7, 16));
        }
    }

    public class ArrayListDictionary
    {
        public ArrayListDictionary()
        {

        }

        public static int GetTotal(int a, int b, int c, int d, int e)
        {
            int[] intArray = { a + 5, b + 5, c + 5, d + 5, e + 5 };

            List<int> intList = new List<int>();

            foreach (var item in intArray)
            {
                intList.Add((int)Math.Pow(item, 2));
            }

            Dictionary<int, int> intDictionary = new Dictionary<int, int>();

            int i = 1;
            foreach (var item in intList)
            {
                intDictionary.Add(i, item - 10);
                i++;
            }

            int total = 0;
            foreach(KeyValuePair<int,int> pair in intDictionary)
            {
                total += pair.Value;
            }
            return total;
        }
    }
}
