using EmployeeClient.EmployeeService;
using System;
using System.Collections.Generic;

namespace EmployeeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeService.EmployeeService employeeService = new EmployeeService.EmployeeService();

            // Why don't these work? <----------------------------------------------

            //Employee e1 = new Employee(1, "Margaret", "123");
            //Employee e2 = new Employee(2, "Jimmy", "456");
            //Employee e3 = new Employee(3, "Garet", "789");

            Employee e1 = employeeService.CreateEmployee(1, "Margaret", "123");
            Employee e2 = employeeService.CreateEmployee(2, "Jimmy", "456");
            Employee e3 = employeeService.CreateEmployee(3, "Garet", "789");

            employeeService.AddManager(e1);
            employeeService.AddEmployee(e1, e2);
            employeeService.AddEmployee(e1, e3);

            Employee man = employeeService.GetManagerOf(e2);
            Console.WriteLine("Manager #" + man.ID + ": " + man.Name + " (" + man.SSN + ")");
            Employee[] result = employeeService.GetEmployeesOf(e1);
            foreach (Employee emp in result)
            {
                Console.WriteLine("Employee #" + emp.ID + ": " + emp.Name + " (" + emp.SSN + ")");
            }
            Console.ReadKey();
        }
    }
}
