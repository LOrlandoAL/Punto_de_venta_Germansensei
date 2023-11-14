using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta_de_German
{
    public partial class FrmPuntodeVenta : MetroFramework.Forms.MetroForm
    {
        public FrmPuntodeVenta()
        {
            InitializeComponent();
            Bitmap img = new Bitmap(Application.StartupPath + @"\imgGerman\ExitSales.jpg");
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
