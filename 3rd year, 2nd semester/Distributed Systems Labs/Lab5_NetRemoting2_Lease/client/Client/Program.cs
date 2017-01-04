using System;
using System.Runtime.Remoting;
using MathLibrary;
using System.Runtime.Remoting.Lifetime;
using System.Threading;
namespace MathClient
{
    class ClientMain
    {
        static void Main(string[] args)
        { // Load the configuration file
            RemotingConfiguration.Configure("Client.exe.config");
            // Get a proxy to the remote object
            SimpleMath sm = new SimpleMath();
            Console.WriteLine("Lifetime: {0}", ((ILease)(sm.GetLifetimeService())).CurrentLeaseTime);

            // Use the remote object
            Console.WriteLine("5 + 2 = {0}", sm.Add(5, 7));
            Console.WriteLine("Lifetime: {0}", ((ILease)(sm.GetLifetimeService())).CurrentLeaseTime);

            Console.WriteLine("5 - 2 = {0}", sm.Add(4, 8));
            Console.WriteLine("Lifetime: {0}", ((ILease)(sm.GetLifetimeService())).CurrentLeaseTime);

            Thread.Sleep(15000);

            Console.WriteLine("5 - 2 = {0}", sm.Add(4, 9));
            Console.WriteLine("Lifetime: {0}", ((ILease)(sm.GetLifetimeService())).CurrentLeaseTime);

            // Ask user to press Enter
            Console.Write("Press enter to end");
            Console.ReadLine();
        }
    }
}