using Daos;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daos
{
    public class CustomersDAO
    {
        public List<Customers> GetAll()
        {
            List<Customers> lista = new List<Customers>();
            //Conectarme
            if (Conexion.Conectar())
            {
                try
                {
                    String select = "Select CustomerID, CompanyName,City,Country from Customers;";
                    DataTable dt = new DataTable();
                    MySqlCommand sentencia = new MySqlCommand();
                    sentencia.CommandText = select;
                    sentencia.Connection = Conexion.conexion;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = sentencia;
                    da.Fill(dt);
                    foreach (DataRow fila in dt.Rows)
                    {
                        Customers s = new Customers(fila["CustomerID"].ToString(), fila["CompanyName"].ToString(), fila["City"].ToString(), fila["Country"].ToString());
                        lista.Add(s);
                    }

                    return lista;
                }
                finally
                {
                    Conexion.Desconectar();
                }
            }
            else
            {
                return null;
            }

        }
    }
}
