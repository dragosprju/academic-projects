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

namespace Lab3_MSMQ_LiveNotepad
{
    public partial class LiveNotepadForm : Form
    {
        public LiveNotepadForm()
        {
            InitializeComponent();

        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (connectButton.Text.Equals("Connect"))
            {
                MessageEngine.connect();
                connectButton.Text = "Disconnect";
            }
            else
            {
                MessageEngine.disconnect();
                connectButton.Text = "Connect";
            }
            
        }

        delegate void changeTextCallback(string text);

        public void changeText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox.InvokeRequired)
            {
                changeTextCallback callback = new changeTextCallback(changeText);
                this.Invoke(callback, new object[] { text });
            }
            else
            {
                this.textBox.Text = text;
            }
        }

        delegate void logCallback(string text);

        public void log(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.logBox.InvokeRequired)
            {
                logCallback callback = new logCallback(log);
                this.Invoke(callback, new object[] { text });
            }
            else
            {
                this.logBox.Text += text + Environment.NewLine;
                logBox.SelectionStart = logBox.TextLength;
                logBox.ScrollToCaret();
            }
        }

        delegate void titleCallback(string text);

        public void changeTitle(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                titleCallback callback = new titleCallback(changeTitle);
                this.Invoke(callback, new object[] { text });
            }
            else
            {
                this.Text = "Live Notepad " + text;
            }
        }

        private void LiveNotepadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connectButton.Text != "Connect")
            {
                MessageEngine.disconnect();
            }           
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            MessageEngine.sendUpdate(textBox.Text);
        }

        public string getText()
        {
            return textBox.Text;
        }
    }
}
