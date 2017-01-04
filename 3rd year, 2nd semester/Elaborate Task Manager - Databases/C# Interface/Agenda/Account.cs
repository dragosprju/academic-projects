using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agenda
{
    public class Account
    {
        public int ID;
        public string name;
        public string surname;
        public string username;
        public string password;

        public Account()
        {

        }

        public Account(string name, string surname, string username, string password)
        {
            this.name = name;
            this.surname = surname;
            this.username = username;
            this.password = password;
        }

        public void setFirstPart(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public void setSecondPart(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
