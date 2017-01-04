using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace PongServer
{
    class ServerMain
    {
        static void Main(string[] args)
        {
            HttpChannel channel = new HttpChannel(10000);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(PongLibrary.PingPong), "MyURI.soap", WellKnownObjectMode.Singleton);
            //RemotingConfiguration.Configure("PongServer.exe.config", false);

            Console.WriteLine("Server started. Press Enter to end ...");
            Console.ReadLine();
        }
    }
}
