using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Customers
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Customers(String customerid, string companyName, string city, string country)
        {
            CustomerID = customerid;
            CompanyName = companyName;
            City = city;
            Country = country;
        }
        public Customers()
        {
            CustomerID = "";
            CompanyName = "";
            City = "";
            Country = "";
        }
    }
}
