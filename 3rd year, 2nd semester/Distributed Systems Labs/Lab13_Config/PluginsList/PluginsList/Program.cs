using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Plugins;

namespace Lab13_Config
{

    class Program
    {
        const string mySection = "plugins/pluginInfo";

        static void Main(string[] args)
        {
            PluginInfo pluginInfo = (PluginInfo)ConfigurationManager.GetSection(mySection);
            Console.WriteLine(pluginInfo.ToString());
            Console.WriteLine("Plugins: ");
            for (int i = 0; i < pluginInfo.Plugins.Count; i++)
            {
                Console.WriteLine("- " + pluginInfo.Plugins[i]);
            }
            
            Console.ReadKey();
        }
    }
}
