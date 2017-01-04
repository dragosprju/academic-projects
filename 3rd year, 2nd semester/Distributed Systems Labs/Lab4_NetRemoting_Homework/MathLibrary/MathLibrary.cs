using System;
using System.Collections.Generic;

namespace MathLibrary
{
    public class SimpleMath : MarshalByRefObject
    {
        public SimpleMath()
        {
            Console.WriteLine("SimpleMath ctor called");
        }

        public int Add(int n1, int n2)
        {
            Console.WriteLine("SimpleMath.0({Add}, {1})", n1, n2);
            return n1 + n2;
        }

        public int Subtract(int n1, int n2)
        {
            Console.WriteLine("SimpleMath.Subtract({0}, {1})", n1, n2);
            return n1 - n2;
        }

        public int[] Sort(int[] items) {
            Console.WriteLine("SimpleMath.Sort(..)");
            List<int> itemsList = new List<int>(items);
            itemsList.Sort();
            items = itemsList.ToArray();
            return items;
        }

        public int Find(int[] items, int item)
        {
            Console.WriteLine("SimpleMath.Find(.., {0})", item);
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public int[] Delete(int[] items, int item)
        {
            Console.WriteLine("SimpleMath.Delete(.., {0})", item);
            List <int> itemsList = new List<int>();
            bool result;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != item)
                {
                    itemsList.Add(items[i]);
                }                
            }

            items = itemsList.ToArray();
            return items;
        }

    }
}
