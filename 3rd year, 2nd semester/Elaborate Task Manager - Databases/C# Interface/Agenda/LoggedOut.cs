using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Agenda
{
    public static class LoggedOut
    {
        static private Account newAccount = null;

        static public bool login(string username, string password)
        {
            string encryptedPassword = null;

            try
            {
                encryptedPassword = Convert.ToString(DatabaseWrapper.Select("SELECT parola FROM utilizator WHERE nume_utilizator = '" + username + "'")[0][0]);
            }
            catch (Exception ex) {
                //MessageBox.Show(ex.Message);
            }

            if (encryptedPassword == null)
            {
                MessageBox.Show("Numele de utilizator nu există. Vă rugăm să încercați din nou.");
                return false;
            }

            string decryptedPassword = AESThenHMAC.SimpleDecryptWithPassword(encryptedPassword, password);

            if (decryptedPassword != null)
            {
                List<List<object>> result = DatabaseWrapper.Select("SELECT responsabil.id, responsabil.prenume, responsabil.nume FROM utilizator, responsabil WHERE utilizator.id = responsabil.id AND nume_utilizator = '" + username + "'");
                LoggedIn.connectedAccount = new Account();
                LoggedIn.connectedAccount.ID = Convert.ToInt32(result[0][0]);
                LoggedIn.connectedAccount.name = Convert.ToString(result[0][1]);
                LoggedIn.connectedAccount.surname = Convert.ToString(result[0][2]);
                LoggedIn.connectedAccount.username = username;
                return true;
            }
            else
            {
                MessageBox.Show("Parolă incorectă");
                return false;
            }
        }

        static public void setFirstPart(string name, string surname)
        {
            newAccount = new Account();
            newAccount.setFirstPart(name, surname);
        }

        static public void setSecondPart(string username, string password)
        {
            newAccount.setSecondPart(username, password);
        }

        static public bool createAccount()
        {
            List<List<object>> result = DatabaseWrapper.Select("SELECT nume_utilizator FROM utilizator WHERE utilizator.nume_utilizator = '" + newAccount.username + "'");
            if (result.Count > 0)
            {
                MessageBox.Show("Numele de utilizator inserat deja exista. Încercați unul nou.");
                return false;
            }

            DatabaseWrapper.Execute("INSERT INTO responsabil(prenume, nume) VALUES('" + newAccount.name + "', '" + newAccount.surname + "')");
            int responsible_ID = Convert.ToInt32(DatabaseWrapper.Select("SELECT id FROM responsabil WHERE id = (SELECT MAX(id) FROM responsabil)")[0][0]);
            string encryptedPassword = AESThenHMAC.SimpleEncryptWithPassword("check", newAccount.password);
            DatabaseWrapper.Execute("INSERT INTO utilizator VALUES('" + responsible_ID.ToString() + "', '" + newAccount.username + "', '" + encryptedPassword + "')");
            newAccount = null;

            return true;
        }
        
        static public void cancel()
        {
            newAccount = null;
        }
    }
}
