using System;
using System.Configuration.Install;
using System.Collections;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Collections.Specialized;

namespace HelloWorld_ToInstall
{
    [RunInstaller(true)]
    public partial class InstallerHelper : Installer
    {
        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
            AddConfigurationFileDetails();
        }

        /// <summary>
        /// Updates the configuration file for deployed service
        /// </summary>
        private void AddConfigurationFileDetails()
        {
            try
            {

                // In order to get the value from the textBox named ‘EDITA1’ I needed to add the line:
                // ‘/PathValue = [EDITA1]’ to the CustomActionData property of the CustomAction We added. 
                string InstallDir = Context.Parameters["InstallDir"];
                string Message = Context.Parameters["Msg"];

                // Get the path to the executable file that is being installed on the target computer
                string assemblypath = Context.Parameters["assemblypath"];
                string appConfigPath = assemblypath + ".config";

                // Write the path to the app.config file
                XmlDocument doc = new XmlDocument();
                doc.Load(appConfigPath);                

                XmlNode configuration = null;
                foreach (XmlNode node in doc.ChildNodes)
                    if (node.Name == "configuration")
                        configuration = node;


                if (configuration != null)
                {
                    XmlNode appSettingsNode = null;
                    foreach (XmlNode node in configuration.ChildNodes)
                    {
                        if (node.Name == "appSettings")
                            appSettingsNode = node;
                    }

                    if (appSettingsNode != null)
                    {
                        XmlElement mainElement = appSettingsNode["add"];

                        XmlAttribute keyAttribute = mainElement.Attributes["key"];
                        XmlAttribute valueAttribute = mainElement.Attributes["value"];

                        //Reassign values in the config file
                        keyAttribute.Value = "messageText";
                        valueAttribute.Value = Message;

                        doc.Save(appConfigPath);
                    }
                }
            }
            catch (FormatException e)
            {
                string s = e.Message;
                MessageBox.Show(s);
            }
        }
    }
}
