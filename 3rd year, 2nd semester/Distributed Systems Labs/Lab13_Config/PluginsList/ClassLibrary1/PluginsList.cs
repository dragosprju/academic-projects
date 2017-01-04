using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace Plugins
{
    public class SectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object context, XmlNode section)
        {
            string a = section["assemblyName"].InnerText;
            string t = section["typeName"].InnerText;
            bool i = Convert.ToBoolean(section["isActive"].InnerText);
            List<string> plugins = new List<string>();
            int count = section["plugins"].ChildNodes.Count;
            for (int id = 0; id < count; id++)
            {
                plugins.Add(section["plugins"].ChildNodes[id].Attributes["name"].Value.ToString());
            }
            PluginInfo toReturn = new PluginInfo(a, t, i);
            toReturn.Plugins = plugins;
            return toReturn;
        }
    }
    public class PluginInfo
    {
        private string assemblyName;
        private string typeName;
        private bool isActive;

        public PluginInfo(string assemblyName, string typeName, bool isActive)
        {
            this.assemblyName = assemblyName;
            this.typeName = typeName;
            this.isActive = isActive;
        }

        public string AssemblyName
        {
            get { return this.assemblyName; }
        }

        public string TypeName
        {
            get { return this.typeName; }
        }

        public bool IsActive
        {
            get { return this.isActive; }
        }

        public override string ToString()
        {
            return String.Format("Assembly name = {0}\nType named = {1}\nIs active = {2}", this.assemblyName, this.typeName, this.isActive);
        }

        private IList plugins;
        public IList Plugins
        {
            get
            {
                return this.plugins;
            }

            set
            {
                this.plugins = value;
            }
        }
    }
}
