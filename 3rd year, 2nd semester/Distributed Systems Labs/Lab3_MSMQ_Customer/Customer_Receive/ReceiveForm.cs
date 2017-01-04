using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CustomerLibrary;

namespace Customer_Receive
{
    public partial class ReceiveForm : Form
    {

        private bool xmlMessageFormatting = true;

        public ReceiveForm()
        {
            InitializeComponent();
        }

        private void xmlCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (xmlCheckBox.Checked == true)
            {
                binaryCheckBox.Checked = false;
                xmlMessageFormatting = true;
            }
            else
            {
                binaryCheckBox.Checked = true;
                xmlMessageFormatting = false;
            }
        }

        private void binaryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (binaryCheckBox.Checked == true)
            {
                xmlCheckBox.Checked = false;
                xmlMessageFormatting = false;
            }
            else
            {
                xmlCheckBox.Checked = true;
                xmlMessageFormatting = true;
            }
        }

        private void receiveButton_Click(object sender, EventArgs e)
        {
            MessageQueue mq = new MessageQueue(@".\private$\DragosPerjuQ");
            Type[] expectedTypes = new Type[] { typeof(Customer) };
            Customer customer = new Customer();
            bool received = false;

            if (xmlMessageFormatting == true)
            {
                mq.Formatter = new XmlMessageFormatter(expectedTypes);
            }
            else
            {
                mq.Formatter = new BinaryMessageFormatter();
            }
            
            try
            {
                System.Messaging.Message msg = mq.Receive(TimeSpan.FromSeconds(1));
                customer = (Customer)msg.Body;
                received = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }

            if (received == true)
            {
                if (customer.Name != null)
                {
                    custNameTextBox.Text = customer.Name;
                }
                if (customer.Email != null)
                {
                    custEmailTextBox.Text = customer.Email;
                }          
            }
            
        }
    }
}
