using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Productos
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double CantidadAPedir { get; set; }
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public Productos(int productID, string productName, int supplierID, string companyName, int categoryID,
            string categoryName, string quantityPerUnit, double unitPrice, int unitsInStock, int unitsOnOrder,
            int reorderLevel, bool discontinued)
        {
            ProductID = productID;
            ProductName = productName;
            SupplierID = supplierID;
            CompanyName = companyName;
            CategoryID = categoryID;
            CategoryName = categoryName;
            QuantityPerUnit = quantityPerUnit;
            UnitPrice = unitPrice;
            UnitsInStock = unitsInStock;
            UnitsOnOrder = unitsOnOrder;
            ReorderLevel = reorderLevel;
            Discontinued = discontinued;
        }
        public Productos()
        {
            ProductID = 0;
            ProductName = "";
            SupplierID = -1;
            CompanyName = "";
            CategoryID = -1;
            CategoryName = "";
            QuantityPerUnit = "";
            UnitPrice = 0;
            UnitsInStock = 0;
            UnitsOnOrder = 0;
            ReorderLevel = 0;
            Discontinued = false;
            CantidadAPedir = 0;
        }

    }
}
