using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Productos
{
    public partial class Form1 : Form
    {
        public string nombreValor = "AAAAAAAAAAAAAAA";
        public int codigoValor = 0;
        public int cantidadValor = 0;
        public string descripcionValor = "";
        public double precioValor = 0.0;
        public string tipoValor = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void insertarNuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            a.Visible = true;
            nombreValor = a.nombre;
            codigoValor = a.codigo;

            Console.WriteLine(nombreValor);
        }
    }
}
