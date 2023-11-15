using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class venta
    {
        public int id { get; set; }
        public string Product { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }

        public venta(int Id, string product, string name, int cantidad, double precio, double Total)
        {
            id = Id;
            this.Product = product;
            ProductName = name;
            Quantity = cantidad;
            Price = precio;
            this.Total = Total;
        }

    }
}
