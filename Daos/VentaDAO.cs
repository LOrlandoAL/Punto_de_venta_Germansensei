using Daos;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daos
{
    public class ventaDAO
    {
        public int InsertarOrden(string customerID, int employeeID, int shipperID)
        {
            //Conectarme
            if (Conexion.Conectar())
            {
                try
                {
                    //Crear la sentencia a ejecutar (INSERT)
                    String select = @"INSERT INTO ORDERS (CUSTOMERID,EMPLOYEEID, ORDERDATE, SHIPVIA) 
                                    VALUES(
                                        @customerId, @employeeId, @orderDate, @shipVia); 
                                    select last_insert_id();";

                    // Se modifico la manera de crear la sentencia de ejecución porque por alguna razón
                    // generaba una excepción al signar nulos
                    MySqlCommand sentencia = new MySqlCommand(select, Conexion.conexion);
                    //sentencia.CommandText = select;

                    sentencia.Parameters.AddWithValue("@customerId", customerID);
                    sentencia.Parameters.AddWithValue("@employeeId", employeeID);
                    sentencia.Parameters.AddWithValue("@orderDate", DateTime.Now);
                    sentencia.Parameters.AddWithValue("@shipVia", shipperID);
                    sentencia.Connection = Conexion.conexion;

                    //Ejercutar el comando 
                    //Cuando nos interesa obtener un valor adicional en el comando (como en el ejemplo de arriba que obtiene el último id generado por autoincrement podemos usar ExecuteScalar
                    int claveNuevoProducto = Convert.ToInt32(sentencia.ExecuteScalar());

                    //O de lo contrario podríamos usar ExecuteNonQuery que simplemente ejecuta la sentencia y nos permite recuperar (solo si nos interesa) el número de filas afectadas (si es un insert nos regresa cuantas filas agregó, en un update cuantas filas editó y en un delete igual cuantas filas eliminó, por ejemplo:
                    //int filasAfectadas = Convert.ToInt32(sentencia.ExecuteNonQuery());


                    return claveNuevoProducto;
                }
                finally
                {
                    Conexion.Desconectar();
                }
            }
            else
            {
                //Devolvemos un cero indicando que no se insertó nada
                return 0;
            }
        }

        public int InsertarOrderDetails(int id, venta v, int UnitsInStock)
        {
            //Conectarme
            if (Conexion.Conectar())
            {
                try
                {
                    //Crear la sentencia a ejecutar (INSERT)
                    String select = @"INSERT INTO `order details` (ORDERID, PRODUCTID, UNITPRICE, QUANTITY, DISCOUNT) 
                                    VALUES(@orderId, @productId, @unitPrice, @quantity, @discount);";

                    // Se modifico la manera de crear la sentencia de ejecución porque por alguna razón
                    // generaba una excepción al signar nulos
                    MySqlCommand sentencia = new MySqlCommand(select, Conexion.conexion);
                    //sentencia.CommandText = select;

                    sentencia.Parameters.AddWithValue("@orderId", id);
                    sentencia.Parameters.AddWithValue("@productId", int.Parse(v.Product));
                    sentencia.Parameters.AddWithValue("@unitPrice", v.Price);
                    sentencia.Parameters.AddWithValue("@quantity", v.Quantity);
                    sentencia.Parameters.AddWithValue("@discount", 0);
                    sentencia.Connection = Conexion.conexion;

                    //Ejercutar el comando 
                    //Cuando nos interesa obtener un valor adicional en el comando (como en el ejemplo de arriba que obtiene el último id generado por autoincrement podemos usar ExecuteScalar
                    int claveNuevoProducto = Convert.ToInt32(sentencia.ExecuteScalar());

                    //O de lo contrario podríamos usar ExecuteNonQuery que simplemente ejecuta la sentencia y nos permite recuperar (solo si nos interesa) el número de filas afectadas (si es un insert nos regresa cuantas filas agregó, en un update cuantas filas editó y en un delete igual cuantas filas eliminó, por ejemplo:
                    //int filasAfectadas = Convert.ToInt32(sentencia.ExecuteNonQuery());

                    //Crear la sentencia a ejecutar (INSERT)
                    String update = @"UPDATE products
                                    SET UnitsInStock = UnitsInStock - @UnitsInStock
                                    WHERE ProductID = @ProductID;";

                    // Se modifico la manera de crear la sentencia de ejecución porque por alguna razón
                    // generaba una excepción al signar nulos
                    MySqlCommand sentenciaUpdate = new MySqlCommand(update, Conexion.conexion);
                    //sentencia.CommandText = select;
                    sentenciaUpdate.Parameters.AddWithValue("@productId", int.Parse(v.Product));
                    sentenciaUpdate.Parameters.AddWithValue("@UnitsInStock", UnitsInStock);
                    sentenciaUpdate.Connection = Conexion.conexion;

                    //Ejercutar el comando 
                    //Cuando nos interesa obtener un valor adicional en el comando (como en el ejemplo de arriba que obtiene el último id generado por autoincrement podemos usar ExecuteScalar
                    Convert.ToInt32(sentenciaUpdate.ExecuteScalar());
                    return claveNuevoProducto;
                }
                finally
                {
                    Conexion.Desconectar();
                }
            }
            else
            {
                //Devolvemos un cero indicando que no se insertó nada
                return 0;
            }
        }

    }
}
