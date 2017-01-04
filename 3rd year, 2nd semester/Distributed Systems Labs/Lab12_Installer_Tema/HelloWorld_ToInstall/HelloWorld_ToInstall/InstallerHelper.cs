using System;
using System.Configuration.Install;
using System.Collections;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Collections.Specialized;
using Microsoft.Win32;

namespace HelloWorld_ToInstall
{
    [RunInstaller(true)]
    public partial class InstallerHelper : Installer
    {
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            try {
                CheckSerial();                
            }
            catch (InstallException e)
            {
                throw e;
            }
            base.OnBeforeInstall(savedState);
        }

        private void CheckSerial()
        {
            string Serial = Context.Parameters["Serial"];
            string Manufacturer = Context.Parameters["Manufacturer"];

            string regDir = @"SOFTWARE\" + Manufacturer + @"\";
            try
            {
                using (RegistryKey Key = Registry.CurrentUser.OpenSubKey(regDir, true))
                {
                    if (Key != null)
                    {
                        object val = Key.GetValue("Serial");

                        if (val == null)
                        {
                            throw new Exception("Serial missing");
                        }
                        else
                        {
                            if (val.ToString() == Serial)
                            {
                                MessageBox.Show("Serial checked");
                            }
                            else
                            {
                                throw new InstallException("Serial invalid");
                            }
                            
                        }
                    }
                }
            }
            catch (InstallException e)
            {
                throw e;
            }
        }
    }
}
