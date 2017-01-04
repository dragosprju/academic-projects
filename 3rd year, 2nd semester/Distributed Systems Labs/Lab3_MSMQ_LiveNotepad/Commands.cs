using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_MSMQ_LiveNotepad
{
    public static class Commands
    {
        public const string imNew = "imNew";
        public const string imNewToo = "imNewToo";
        public const string ohHi = "ohHi";
        public const string update = "update";
        public const string seeYa = "seeYa";

        private static string extractInformation(string label, int i)
        {
            string[] information = label.Split(' ');
            if (information.Length < 2)
            {
                throw new Exception("The label isn't in a format of '[COMMAND] [RECIPIENT]'.");
            }
            else
            {
                return information[i];
            }
        }

        public static string extractCommand(System.Messaging.Message msg)
        {
            try {
                return extractInformation(msg.Label, 0);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "extractCommand");
            }
            return null;
        }

        public static string extractRecipient(System.Messaging.Message msg)
        {
            try
            {
                return extractInformation(msg.Label, 1);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "extractRecipient");
            }
            return null;
        }

        public static string extractSender(System.Messaging.Message msg)
        {
            try
            {
                msg.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
                if (Commands.extractCommand(msg) == Commands.imNew || Commands.extractCommand(msg) == Commands.imNewToo)
                {
                    return extractInformation(msg.Label, 1);
                }
                else if (Commands.extractCommand(msg) == Commands.ohHi)
                {                    
                    return msg.Body.ToString().Split(' ')[0];
                }
                else
                {
                    return msg.Body.ToString().Split(new[] { '\r', '\n' })[0];
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "extractRecipient");
            }
            return null;           
        }

        public static string create(string command, string recipient)
        {
            return command + ' ' + recipient;
        }

        public static System.Messaging.Message clientListAndText(string command, string recipient, string myId, List<string> otherIds)
        {
            System.Messaging.Message msg = new System.Messaging.Message();
            msg.Label = create(command, recipient);
            msg.Body = myId + " ";
            msg.Body += string.Join(" ", otherIds);
            msg.Body += Environment.NewLine + Program.form.getText();

            return msg;
        }

        public static List<string> extractClientList(System.Messaging.Message msg)
        {
            msg.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
            return msg.Body.ToString().Split(new[] { '\r', '\n' })[0].Split(' ').ToList();
        }

        public static string extractText(System.Messaging.Message msg)
        {
            if (Commands.extractCommand(msg) == Commands.ohHi)
            {
                if (msg.Body.ToString().Split('\n').Length > 1)
                {
                    return msg.Body.ToString().Split('\n')[1];
                }                
            }
            else if (Commands.extractCommand(msg) == Commands.imNew || Commands.extractCommand(msg) == Commands.update)
            {
                return msg.Body.ToString();
            }
            return null;
        }
    }
}
