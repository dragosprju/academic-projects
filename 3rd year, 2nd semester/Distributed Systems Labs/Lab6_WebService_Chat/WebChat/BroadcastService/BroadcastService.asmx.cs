using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BroadcastService
{
    /// <summary>
    /// Summary description for BroadcastService
    /// </summary>
    [WebService(Namespace = "localhost")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BroadcastService : System.Web.Services.WebService
    {
        private static Dictionary<int, List<string>> mailbox = new Dictionary<int, List<string>>();

        [WebMethod]
        public int Register()
        {
            int newID = Guid.NewGuid().GetHashCode();
            mailbox.Add(newID, new List<string>());
            return newID;
        }

        [WebMethod]
        public void Unregister(int ID)
        {
            mailbox.Remove(ID);
        }

        [WebMethod]
        public int PutInMailbox(int ID, string message)
        {
            List<int> missingIDs = new List<int>();
            if (mailbox.ContainsKey(ID))
            {
                mailbox[ID].Add(message);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        [WebMethod]
        public List<string> CheckMailbox(int ID)
        {
            List<string> toReturn = mailbox[ID];
            mailbox[ID] = new List<string>();
            return toReturn;
        }

        [WebMethod]
        public void ClearEverything()
        {
            mailbox = new Dictionary<int, List<string>>();
            
        }
    }
}
