using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daos;
using Datos;
using Modelos;

namespace Punto_de_Venta_de_German
{
    public partial class FrmPuntodeVenta : Form
    {
        //Decllaracion de listas paara llenarlas de datos mas adelante.
        public static List<Productos> Productos;
        public static List<Customers> Clientes;
        public FrmPuntodeVenta()
        {
            InitializeComponent();
            lblFecha.Text = DateTime.Now.ToString(); 
            EmployeeDAO Dao = new EmployeeDAO();
            Employee emp = Dao.GetAll();
            ActualizarTabla();

            lblEmpleado.Text = emp.FullName;

        }

        private void FrmPuntodeVenta_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void ActualizarTabla()
        {
            Conexion con = new Conexion();
            Productos = new ProductDAO().GetAll();
            Clientes = new CustomersDAO().GetAll();

            // Ordenar productos por nombre
            Productos = Productos.OrderBy(p => p.ProductName).ToList();
            cbCustomer.DataSource = Clientes;
            cbCustomer.DisplayMember = "CompanyName";
        }


        private void Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BuscarPorCodigo();
            }
        }

        private void BuscarPorCodigo() 
        {
            ProductDAO dao = new ProductDAO();
            Productos p = new Productos();
            p = dao.ObtenerProductoPorCodigo(txtCodigo.Text);

            if (p != null)
            {
                MessageBox.Show(p.ProductName + " " + p.UnitPrice);
            }
            else
            {
                MessageBox.Show("Algo salio mal");
            }
        }

        private void BotonBuscar(object sender, EventArgs e)
        {
            BuscarPorCodigo();
        }
    }
}
