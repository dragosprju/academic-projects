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

namespace Customer_Send
{
    public partial class SendForm : Form
    {

        private bool xmlMessageFormatting = true;

        public SendForm()
        {
            InitializeComponent();
        }

        private void xmlCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(xmlCheckBox.Checked == true)
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
            if(binaryCheckBox.Checked == true)
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

        private void sendButton_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer(custNameTextBox.Text, custEmailTextBox.Text, custCardNoTextBox.Text);            

            if (!MessageQueue.Exists(@".\private$\DragosPerjuQ"))
            {
                MessageQueue.Create(@".\private$\DragosPerjuQ");
            }

            MessageQueue mq = new MessageQueue(@".\private$\DragosPerjuQ");
            System.Messaging.Message msg = new System.Messaging.Message();

            if (xmlMessageFormatting == false)
            {
                msg.Formatter = new BinaryMessageFormatter();
            }

            msg.Label = "Customer";
            msg.Body = customer;

            try
            {
                mq.Send(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            custNameTextBox.Text = "";
            custEmailTextBox.Text = "";
            custCardNoTextBox.Text = "";
        }

        private void cleanButton_Click(object sender, EventArgs e)
        {
            if (!MessageQueue.Exists(@".\private$\DragosPerjuQ"))
            {
                MessageQueue.Delete(@".\private$\DragosPerjuQ");
            }
        }
    }
}
