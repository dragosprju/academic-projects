using System;
using System.Runtime.Remoting; // General remoting stuff
namespace MathServer
{
    class ServerMain
    {
        static void Main(string[] args)
        { // Read remoting info from config file.
            RemotingConfiguration.Configure("Server.exe.config");
            // Keep the server alive until Enter is pressed.
            Console.WriteLine("Server started. Press Enter to end ...");
            Console.ReadLine();
        }
    }
}