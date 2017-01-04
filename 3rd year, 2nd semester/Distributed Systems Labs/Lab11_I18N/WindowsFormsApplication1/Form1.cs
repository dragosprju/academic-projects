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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "en")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }
            else if (comboBox1.Text == "ro") {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ro-RO");
            }

            label1.Text = rm.GetString("language");
            button1.Text = rm.GetString("set");

            MessageBox.Show(rm.GetString("langSet"));
        }
    }
}
