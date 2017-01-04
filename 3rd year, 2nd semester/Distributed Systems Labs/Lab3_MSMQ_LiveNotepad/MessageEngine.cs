using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_MSMQ_LiveNotepad
{
    public static class MessageEngine
    {
        private static string instanceID = Guid.NewGuid().ToString();
        private static string serverID = null;

        private static MessageQueue messageQueue;
        private static bool signedUp = false;
        private static bool stopped = false;

        private static List<string> clientIDs = new List<string>();
        private static Thread loop;
        //private static IAsyncResult onMsgPresentHandle;

        public static void connect()
        {
            Program.form.changeTitle(instanceID);

            if (!MessageQueue.Exists(@".\private$\DragosPerjuLN"))
            {
                MessageQueue.Create(@".\private$\DragosPerjuLN");
            }

            messageQueue = new MessageQueue(@".\private$\DragosPerjuLN");
            stopped = false;
            loop = new Thread(run);
            loop.Start();        
        }

        public static void disconnect()
        {
            stopped = true;
            foreach (string client in clientIDs)
            {
                Program.log("Sent seeYa to " + client + ".");
                sendMessage(Commands.seeYa, client);
            } 
            Program.form.changeTitle("");
            Program.log("Disconnected.");
        }

        private static void sendMessage(string command, string recipient)
        {
            if (command == Commands.imNew || command == Commands.imNewToo)
            {
                sendMessage(command, recipient, null);
            }
            else if (command == Commands.seeYa)
            {
                sendMessage(command, recipient, instanceID);
            }
        }

        private static void sendMessage(System.Messaging.Message msg)
        {
            messageQueue.Send(msg);
        }

        private static void sendMessage(string command, string recipient, string body)
        {
            System.Messaging.Message msg = new System.Messaging.Message();

            msg.Label = Commands.create(command, recipient);
            msg.Body = body;
            msg.TimeToBeReceived = TimeSpan.FromSeconds(5);
            msg.UseDeadLetterQueue = false;

            messageQueue.Send(msg);
        }

        public static void sendUpdate(string text)
        {
            foreach(string client in clientIDs)
            {
                sendMessage(Commands.update, client, text);
            }            
        }

        private static void run()
        {
            Program.log("Connecting...");
            System.Messaging.Message msg = null;
            System.Messaging.Message msg2 = null;

            try
            {
                msg = messageQueue.Peek(TimeSpan.FromMilliseconds(100));
                signedUp = true;

                string sender = Commands.extractRecipient(msg);
                if (Commands.extractCommand(msg) == Commands.imNew && sender != instanceID)
                {
                    bool stolen = false;

                    try
                    {
                        messageQueue.Receive(TimeSpan.FromMilliseconds(100));
                    }
                    catch(Exception)
                    {
                        Program.log("Someone stole the imNew found message from us!");
                        stolen = true;
                    }

                    if (stolen == false)
                    {
                        clientIDs.Add(sender);
                        sendMessage(Commands.imNewToo, instanceID);
                        Program.log("Found " + sender.Substring(0, 8) + ".");

                        serverID = instanceID;
                        Program.log("I'm now the server.");
                    }                    
                    else
                    {
                        signedUp = false;
                        throw new Exception("We'll assume we still haven't signed up with the other clients.");
                    }                        
                }
                else
                {
                    signedUp = false;
                    throw new Exception("We'll assume we still haven't signed up with the other clients.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "We'll assume we still haven't signed up with the other clients.")
                {
                    Program.log("We'll assume we still haven't signed up with the other clients.");
                }
                else if (ex.Message.Contains("Timeout"))
                {
                    Program.log("Didn't find anyone in the queue.");
                }
                else
                {
                    Program.log(ex.Message);
                }
            }

            if (!signedUp)
            {
                Thread imNewThread = new Thread(tryImNew);
                imNewThread.Start();
            }

            while (!stopped)
            {
                try
                {
                    clientIDs = clientIDs.Distinct().ToList();
                    clientIDs.Remove(instanceID);
                    msg = messageQueue.Peek(TimeSpan.FromMilliseconds(100));

                    if (Commands.extractCommand(msg) == Commands.imNew && Commands.extractRecipient(msg) == instanceID)
                    {
                        // Don't do anything about my imNew message. Wait it out to expire. If we're alone, we're gonna listen to new comers;                    
                    }
                    else if ((Commands.extractCommand(msg) == Commands.imNew && Commands.extractRecipient(msg) != instanceID) ||
                        (Commands.extractCommand(msg) == Commands.imNewToo && Commands.extractRecipient(msg) != instanceID) ||
                        Commands.extractRecipient(msg) == instanceID)
                    {
                        // It's a message for us. It either has our recipient ID or it's the second client or a new client. Let's take it.
                        try
                        {
                            msg2 = messageQueue.Receive();
                            if (msg.Id != msg2.Id)
                            {
                                throw new Exception("Peeked message and received message are different.");
                            }
                        }
                        catch (Exception ex)
                        {
                            if (msg2 != null)
                            {
                                sendMessage(msg2);
                            }
                            Program.log(ex.Message);
                        }

                        string sender = Commands.extractSender(msg);
                        switch (Commands.extractCommand(msg))
                        {
                            case Commands.imNew:
                                if (serverID == instanceID)
                                {
                                    clientIDs.Add(sender);
                                    Program.log("Found new client " + sender.Substring(0, 8) + ". Sent him other clients.");

                                    List<string> tempClientIDs = new List<string>(clientIDs);
                                    foreach (string client in tempClientIDs)
                                    {
                                        clientIDs.Remove(client);
                                        sendMessage(Commands.clientListAndText(Commands.ohHi, client, instanceID, clientIDs));
                                        clientIDs.Add(client);
                                    }
                                }
                                break;

                            case Commands.imNewToo:
                                clientIDs.Add(sender);
                                serverID = sender;
                                Program.log("Found the server " + sender.Substring(0, 8) + ".");                                
                                break;

                            case Commands.ohHi:
                                Program.log("Received client list from " + sender.Substring(0, 8) + ":");
                                serverID = sender;

                                clientIDs.Clear();
                                clientIDs.AddRange(Commands.extractClientList(msg));
                                foreach(string client in clientIDs)
                                {
                                    Program.log(client);
                                }
                                Program.form.changeText(Commands.extractText(msg));
                                break;

                            case Commands.update:
                                Program.form.changeText(Commands.extractText(msg));
                                //MessageBox.Show("Update from " + sender.Substring(0, 8));
                                break;

                            case Commands.seeYa:
                                Program.log("Received seeYa from " + sender.Substring(0, 8) + ".");
                                clientIDs.Remove(sender);
                                if (sender == serverID)
                                {
                                    List<string> IDs = new List<string>(clientIDs);
                                    IDs.Add(instanceID);
                                    IDs.Sort();
                                    serverID = IDs[0];

                                    if (serverID != instanceID)
                                    {
                                        Program.log("Changed server to " + serverID.Substring(0, 8) + ".");
                                    }
                                    else
                                    {
                                        Program.log("I'm now server!");
                                    }
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!ex.Message.Contains("Timeout"))
                    {
                        Program.log(ex.Message);
                    }                    
                }
            }
                
        }

        private static void tryImNew()
        {
            for (int i = 0; i < 3 && serverID == null; i++)
            {
                sendMessage(Commands.imNew, instanceID, Program.form.getText());
                Program.log("Sending imNew message.");
                Thread.Sleep(1000);
            }
        }
    }
}
