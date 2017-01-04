using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // Serviciu.Clasa
            SimpleMath.SimpleMath x = new SimpleMath.SimpleMath();

            Console.WriteLine(x.Add(5, 6));
            Console.ReadKey();
        }
    }
}
