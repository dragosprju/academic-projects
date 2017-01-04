using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebChat
{
    public partial class ChatForm : Form
    {
        private bool connected = false;
        private int myID = -1;
        private string myName = "";
        private ReceiveService.ReceiveService receiveService = new ReceiveService.ReceiveService();
        private BroadcastService.BroadcastService broadcastService = new BroadcastService.BroadcastService();

        public ChatForm()
        {
            InitializeComponent();
            receiveService.CookieContainer = new System.Net.CookieContainer();
            chatBox.Text += "** Please insert your nickname below and press 'Connect'..." + Environment.NewLine;
            sendButton.Text = "Connect";
            chatBox.SelectionStart = chatBox.TextLength;
            chatBox.ScrollToCaret();
        }

        private void Send_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                if (!inputBox.Text.Equals(""))
                {
                    int result = receiveService.Logon(inputBox.Text);

                    if (result == -1)
                    {
                        chatBox.Text += "** Nickname is already taken. Please choose a new one..." + Environment.NewLine;
                        inputBox.Text = "";
                    }
                    else if (result == -2)
                    {
                        chatBox.Text += "** Couldn't join the server because the number of maximum connections has been reached. Please try again later." + Environment.NewLine;
                    }
                    else
                    {
                        myID = result;
                        myName = inputBox.Text;
                        inputBox.Text = "";
                        chatBox.Text += "** Connected as '" + myName + "' with session ID #" + myID + Environment.NewLine;
                        pollingTimer.Start();
                        sendButton.Text = "Send";
                        connected = true;
                    }
                }
                else
                {
                    chatBox.Text += "** Nickname must have at least one character" + Environment.NewLine;
                }
            }
            else
            {
                string[] inputSplit = inputBox.Text.Split(' ');
                List<string> inputList = new List<string>(inputSplit);

                if (inputList.Count > 0 && inputList[0].Equals("/pm"))
                {
                    string nickname = inputList[1];
                    string message = "";

                    for (int i = 2; i < inputList.Count; i++)
                    {
                        message += inputList[i];
                        if (i != (inputList.Count - 1))
                        {
                            message += " ";
                        }
                    }

                    int result = receiveService.SendPrivateMessage(nickname, message);

                    if (result == -1)
                    {
                        chatBox.Text += "** Couldn't send message to " + nickname + " because the user isn't connected." + Environment.NewLine;
                    }
                    else if (result == -2)
                    {
                        chatBox.Text += "** Couldn't send message to " + nickname + " because the user was missing in one of the two chat services. The discrepancy has been fixed." + Environment.NewLine;
                    }
                    else
                    {
                        chatBox.Text += "To " + nickname + ": " + message + Environment.NewLine;
                        inputBox.Text = "";
                    }                    
                }
                else if (inputList.Count > 0)
                {
                    string message = inputBox.Text;
                    receiveService.SendMessage(message);

                    chatBox.Text += myName + ": " + message + Environment.NewLine;
                    inputBox.Text = "";
                }
            }
        }

        private void pollingTimer_Tick(object sender, EventArgs e)
        {
            string[] messages = broadcastService.CheckMailbox(myID);
            foreach (string message in messages)
            {
                chatBox.Text += message + Environment.NewLine;
            }
        }

        private void ChatForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send_Click(null, null);
            }
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            ChatForm_KeyDown(sender, e);
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            receiveService.Logout();
        }


    }
}
