using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ReceiveService
{
    /// <summary>
    /// Summary description for ReceiveService
    /// </summary>
    [WebService(Namespace = "localhost")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ReceiveService : System.Web.Services.WebService
    {
        private static BroadcastService.BroadcastService broadcastService = new BroadcastService.BroadcastService();
        private static Dictionary<int, string> clients = new Dictionary<int, string>();
        private const int maxConnections = 3;

        [WebMethod(EnableSession = true)]
        public int Logon(string nickname)
        {
            if (clients.Count == maxConnections)
            {
                return -2;
            }

            foreach (KeyValuePair<int, string> client in clients)
            {
                if (nickname == client.Value)
                {
                    return -1;
                }
            }

            int newID = broadcastService.Register();   
            
            foreach (KeyValuePair<int, string> client in clients)
            {
                broadcastService.PutInMailbox(client.Key, "** " + nickname + " has joined.");
            }         
            clients.Add(newID, nickname);
            Session["ID"] = newID;
            return newID;
        }

        [WebMethod(EnableSession = true)]
        public void SendMessage(string message)
        {
            int senderID = (int)Session["ID"];
            string senderNickname = clients[senderID];
            foreach (KeyValuePair<int, string> client in clients)
            {
                if (client.Key != senderID)
                {
                    int success = broadcastService.PutInMailbox(client.Key, senderNickname + ": " + message);

                    if (success == -1)
                    {
                        clients.Remove(client.Key);
                    }
                }                
            }
        }

        [WebMethod(EnableSession = true)]
        public int SendPrivateMessage(string toNickname, string message)
        {
            foreach(KeyValuePair<int, string> client in clients)
            {
                if (toNickname == client.Value)
                {
                    int success = broadcastService.PutInMailbox(client.Key, "From " + client.Value + ": " + message);

                    if (success == -1)
                    {
                        clients.Remove(client.Key);
                        return -2;
                    }
                    return 0;
                }
            }
            return -1;
        }

        [WebMethod(EnableSession = true)]
        public void Logout()
        {
            int leavingID = (int)Session["ID"];

            broadcastService.Unregister(leavingID);
            foreach (KeyValuePair<int, string> client in clients)
            {
                broadcastService.PutInMailbox(client.Key, "** " + client.Value + " has left.");           
            }
            clients.Remove(leavingID);            
        }

        [WebMethod]
        public string ClearEverything()
        {
            clients = new Dictionary<int, string>();
            broadcastService.ClearEverything();
            return "It's done";
        }
    }
}
