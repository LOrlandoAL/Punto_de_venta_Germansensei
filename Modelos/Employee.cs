using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public String FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string FirstName { get; set; }
        public String LastName { get; set; }
        public String Title { get; set; }
        public String PostalCode { get; set; }
        public String ReportsTo { get; set; }


        public Employee(int employeeId, string firstName, string lastName, string title, string postalCode, string reportsTo)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            Title = title;
            PostalCode = postalCode;
            ReportsTo = reportsTo;
        }

        public Employee()
        {
            FirstName = "";
            EmployeeId = 0;
            FirstName = "";
            LastName = "";
            Title = "";
            PostalCode = "";
            ReportsTo = "";
        }
    }
}
