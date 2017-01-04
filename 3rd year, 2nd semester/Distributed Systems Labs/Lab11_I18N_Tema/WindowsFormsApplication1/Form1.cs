using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private ResourceManager rm;

        public Form1()
        {
            InitializeComponent();
            rm = new ResourceManager("WindowsFormsApplication1.resources", this.GetType().Assembly);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            langBox_SelectedIndexChanged(null, null);
            timer.Start();
        }

        private void saluteButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(rm.GetString("hello"));
        }

        private void langBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] locale = { "en-US", "ro-RO", "it-IT", "es-ES", "fr-FR", "de-DE" };

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(locale[langBox.SelectedIndex]);
            langLabel.Text = rm.GetString("language");
            saluteButton.Text = rm.GetString("salute");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (langBox.SelectedIndex < langBox.Items.Count - 1)
            {
                langBox.SelectedIndex += 1;
            }
            else
            {
                langBox.SelectedIndex = 0;
            }
        }
    }
}
