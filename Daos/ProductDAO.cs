using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Modelos;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Daos;

namespace Datos
{
    public class ProductDAO
    {
        public List<Productos> GetAll()
        {
            List<Productos> lista = new List<Productos>();
            //Conectarme
            if (Conexion.Conectar())
            {
                try
                {
                    //Crear la sentencia a ejecutar (SELECT)
                    String select = @"SELECT p.PRODUCTID, p.PRODUCTNAME, p.SUPPLIERID, s.COMPANYNAME, 
                        p.CATEGORYID, c.CATEGORYNAME, p.QUANTITYPERUNIT, p.UNITPRICE, p.UNITSINSTOCK, 
                        p.UNITSONORDER, p.REORDERLEVEL, p.DISCONTINUED 
                        FROM PRODUCTS p JOIN SUPPLIERS s
                        ON p.SUPPLIERID = s.SUPPLIERID
                        JOIN CATEGORIES c
                        ON p.CATEGORYID = c.CATEGORYID;";

                    DataTable dt = new DataTable();
                    //Crear el dataadapter
                    MySqlCommand sentencia = new MySqlCommand();
                    sentencia.CommandText = select;
                    sentencia.Connection = Conexion.conexion;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = sentencia;
                    //Llenar el datatable
                    da.Fill(dt);
                    //Crear un objeto categoría por cada fila de la tabla y añadirlo a la lista
                    foreach (DataRow fila in dt.Rows)
                    {
                        Productos producto = new Productos(
                            //int.Parse(fila["Clave"].ToString())
                            Convert.ToInt32(fila["PRODUCTID"]),
                            fila["PRODUCTNAME"].ToString(),
                            Convert.ToInt32(fila["SUPPLIERID"]),
                            fila["COMPANYNAME"].ToString(),
                            Convert.ToInt32(fila["CATEGORYID"]),
                            fila["CATEGORYNAME"].ToString(),
                            fila["QUANTITYPERUNIT"].ToString(),
                            Convert.ToDouble(fila["UNITPRICE"]),
                            Convert.ToInt32(fila["UNITSINSTOCK"]),
                            Convert.ToInt32(fila["UNITSONORDER"]),
                            Convert.ToInt32(fila["REORDERLEVEL"]),
                            Convert.ToBoolean(fila["DISCONTINUED"])
                            );
                        lista.Add(producto);
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
        public Productos ObtenerProductoPorCodigo(string Productid)
        {
            if (Conexion.Conectar())
            {
                try
                {
                    string query = "SELECT Productid, ProductName, UnitPrice, UnitsInStock FROM Products WHERE Productid = @Productid";
                    MySqlCommand sentencia = new MySqlCommand();
                    sentencia.Parameters.AddWithValue("@Productid", Productid);
                    sentencia.CommandText = query;
                    sentencia.Connection = Conexion.conexion;

                    using (MySqlDataReader reader = sentencia.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Productos
                            {
                                ProductID = reader.GetInt32("Productid"),
                                ProductName = reader.GetString("ProductName"),
                                UnitPrice = reader.GetDouble("UnitPrice"),
                                UnitsInStock = reader.GetInt32("UnitsInStock")
                            };
                        }
                    }

                    return null;
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
