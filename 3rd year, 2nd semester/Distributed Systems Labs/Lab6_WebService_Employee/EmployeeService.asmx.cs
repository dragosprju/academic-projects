using System.Collections.Generic;
using System.Web.Services;
using System.Xml.Serialization;

namespace EmployeeService
{
    /// <summary>
    /// Summary description for EmployeeService
    /// </summary>
    /// 

    public class Employee
    {
        public string Name;
        public string SSN;
        [XmlAttribute()]
        public int ID;

        // Required by XmlSerializer
        public Employee()
        {
            // Empty
        }

        public Employee(int ID, string Name, string SSN)
        {
            this.ID = ID;
            this.Name = Name;
            this.SSN = SSN;
        }
    }

    public struct IDPair
    {
        public int managerID;
        public int employeeID;

        public IDPair(int managerID, int employeeID)
        {
            this.managerID = managerID;
            this.employeeID = employeeID;
        }
    }

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [WebService(Namespace = "localhost")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class EmployeeService : System.Web.Services.WebService
    {
        private static List<Employee> managers = new List<Employee>();
        private static List<Employee> employees = new List<Employee>();
        private static List<IDPair> managerEmployeeRelationship = new List<IDPair>();

        [WebMethod]
        public Employee CreateEmployee(int ID, string Name, string SSN)
        {
            return new Employee(ID, Name, SSN);
        }

        [WebMethod]
        public void AddManager(Employee e)
        {
            managers.Add(e);
        }

        [WebMethod]
        public void AddEmployee(Employee m, Employee e)
        {
            employees.Add(e);
            managerEmployeeRelationship.Add(new IDPair(m.ID, e.ID));
        }

        [WebMethod]
        public Employee GetManagerOf(Employee e)
        {
            int wantedManagerID = -1;

            foreach (IDPair pair in managerEmployeeRelationship)
            {
                if (e.ID == pair.employeeID)
                {
                    wantedManagerID = pair.managerID;
                    break;
                }
            }

            foreach (Employee manager in managers)
            {
                if (manager.ID == wantedManagerID)
                {
                    return manager;
                }
            }
            return null;
        }

        [WebMethod]
        public List<Employee> GetEmployeesOf(Employee manager)
        {
            List<int> wantedEmployeesIDs = new List<int>();
            List<Employee> toReturn = new List<Employee>();

            foreach (IDPair pair in managerEmployeeRelationship)
            {
                if (manager.ID == pair.managerID)
                {
                    wantedEmployeesIDs.Add(pair.employeeID);
                }
            }

            foreach (int wantedEmployeeID in wantedEmployeesIDs)
            {
                foreach (Employee employee in employees)
                {
                    if (wantedEmployeeID == employee.ID)
                    {
                        toReturn.Add(employee);
                        break;
                    }
                }
            }

            return toReturn;
        }
    }
}
