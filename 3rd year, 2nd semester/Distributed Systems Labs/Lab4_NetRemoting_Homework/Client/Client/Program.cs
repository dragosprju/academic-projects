using System;
using System.Runtime.Remoting;

using MathLibrary;
namespace MathClient
{
    class ClientMain
    {
        static void Main(string[] args)
        { 
            // Load the configuration file
            RemotingConfiguration.Configure("Client.exe.config", false);

            // Get a proxy to the remote object
            SimpleMath math = new SimpleMath();

            // Use the remote object
            //Console.WriteLine("5 + 2 = {0}", math.Add(5, 2));
            //Console.WriteLine("5 - 2 = {0}", math.Subtract(5, 2));

            int[] items = new int[] { 3, 2, 1 };

            items = math.Sort(items);
            Console.Write("Sort: ");
            foreach(int item in items) {
                Console.Write("{0} ", item);
            }
            Console.Write("\r\n");

            int index = math.Find(items, 3);
            Console.WriteLine("Index of number 3 is: {0}", index);

            items = math.Delete(items, 3);
            Console.Write("Delete: ");
            foreach (int item in items)
            {
                Console.Write("{0} ", item);
            }
            Console.Write("\r\n");


            // Ask user to press Enter
            Console.Write("Press enter to end");
            Console.ReadLine();
        }
    }
}