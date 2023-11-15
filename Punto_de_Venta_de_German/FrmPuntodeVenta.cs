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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Punto_de_Venta_de_German
{
    public partial class FrmPuntodeVenta : Form
    {
        private int id = 0;
        //Decllaracion de listas paara llenarlas de datos mas adelante.
        public static List<Productos> Productos;
        public static List<Customers> Clientes;
        private List<venta> CarritoDeCompras = new List<venta>();
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
                AgregarATabla(p);
                //MessageBox.Show(p.ProductName + " " + p.UnitPrice + " " + p.UnitsInStock);

            }
            else
            {
                MessageBox.Show("Algo salio mal");
            }
        }

        private void BotonBuscar(object sender, EventArgs e)
        {
            ProductDAO dao = new ProductDAO();
            Productos p = new Productos();
            BuscarPorCodigo();
        }

        private void AgregarATabla(Productos p)
        {
            bool bandera = false;
            int cantidad = 1;
            id++;
            //int.TryParse(txtCantidad.Text, out cantidad)
            if (cantidad != 0)
            {
                string code = "";
                if (txtCodigo.Text.Length < 13)
                {
                    code = txtCodigo.Text;
                    for (int i = 0; i < 13 - txtCodigo.Text.Length; i++)
                    {
                        code = "0" + code;
                    }
                }

                for (int i = 0; i < CarritoDeCompras.Count; i++)
                {
                    if (Int32.Parse(CarritoDeCompras[i].Product) == p.ProductID)
                    {
                        CarritoDeCompras[i].Quantity += cantidad;


                        if (CarritoDeCompras[i].Quantity > p.UnitsInStock)
                        {
                            DialogResult resultado = MetroFramework.MetroMessageBox.Show(this, "Solo se cuenta con " + p.UnitsInStock + " unidades del producto " + p.ProductName + " ¿desea adquirirlas?", "Confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, 120);
                            if (resultado == DialogResult.OK)
                            {
                                CarritoDeCompras[i].Quantity = p.UnitsInStock;
                            }
                            else
                            {
                                //txtCantidad.Text = "1";
                                return;
                            }

                        }

                        double total2 = CarritoDeCompras[i].Quantity * p.UnitPrice;
                        CarritoDeCompras[i].Total = total2;
                        dgvProductos.DataSource = null;
                        dgvProductos.DataSource = CarritoDeCompras;
                        dgvProductos.Columns["Id"].Visible = false;
                        bandera = true;
                        break;
                    }
                }
                if (bandera)
                {
                    //this.total = 0;
                    for (int i = 0; i < CarritoDeCompras.Count; i++)
                    {
                        //this.total += CarritoDeCompras[i].Total;
                    }
                    //txtCantidad.Text = "1";
                    //lblSubtotal.Text = "Subtotal: $" + this.total;
                    //lblIVA.Text = "IVA: $" + this.total * iva;
                    //lblTotal.Text = "Total: $" + (this.total + this.total * iva);
                }
                else
                {
                    if (cantidad > p.UnitsInStock)
                    {
                        DialogResult resultado = MetroFramework.MetroMessageBox.Show(this, "Solo se cuenta con " + p.UnitsInStock + " unidades del producto " + p.ProductName + " ¿desea adquirirlas?", "Confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, 120);
                        if (resultado == DialogResult.OK)
                        {
                            cantidad = p.UnitsInStock;
                        }
                        else
                        {
                            //txtCantidad.Text = "1";
                            return;
                        }
                    }
                    double total = cantidad * p.UnitPrice;
                    CarritoDeCompras.Add(new venta(id, code, p.ProductName, cantidad, p.UnitPrice, total));
                    dgvProductos.DataSource = null;
                    dgvProductos.DataSource = CarritoDeCompras;
                    dgvProductos.Columns["Id"].Visible = false;
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Cantidad invalida", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, 120);
            }
            //txtProducto.Text = "";
        }
    }
}
