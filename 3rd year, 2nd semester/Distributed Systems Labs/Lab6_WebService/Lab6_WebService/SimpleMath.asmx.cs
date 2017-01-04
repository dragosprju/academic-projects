using System;
using System.Web.Services;

namespace SimpleMath
{
    public class SimpleMath : WebService
    {
        [WebMethod]
        public int Add(int n1, int n2)
        {
            return n1 + n2;
        
        }
        [WebMethod]
        public int Subtract(int n1, int n2)
        {
            return n1 - n2;
        }
    }
}
