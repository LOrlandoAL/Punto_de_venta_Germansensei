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
    public class EmployeeDAO
    {
        public Employee GetAll()
        {
            Employee emp = null;
            //Conectarme
            if (Conexion.Conectar())
            {
                try
                {
                    String select = "SELECT EmployeeId, FirstName, LastName FROM Employees where EmployeeId=1";
                    DataTable dt = new DataTable();
                    MySqlCommand sentencia = new MySqlCommand();
                    sentencia.CommandText = select;
                    sentencia.Connection = Conexion.conexion;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = sentencia;
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow fila = dt.Rows[0];
                        emp = new Employee()
                        {
                            EmployeeId = Convert.ToInt32(fila["EmployeeId"]),
                            FirstName = fila["FirstName"].ToString(),
                            LastName = fila["LastName"].ToString(),
                        };

                    }

                    return emp;
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
