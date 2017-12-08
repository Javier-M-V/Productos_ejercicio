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
        public string mod = "✍";
        public string del = "✗";

        public Form1()
        {
            InitializeComponent();
            TablaDatos.Rows.Add("D3200", 1, 55, "DX format", 455.5, "Cuerpo",mod,del);
        }

        private void insertarNuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            a.Show();

            codigoValor = a.codigo;
            cantidadValor = a.cantidad;
            descripcionValor = a.descripcion;
            precioValor = a.precio;
            tipoValor = a.tipo;


            /*TablaDatos.Rows.["ColumnNombre"].Value = a.nombre;
            TablaDatos.Rows.["ColumCodigo"].Value = a.codigo;
            TablaDatos.Rows.["ColumCantidad"].Value = a.cantidad;
            TablaDatos.Rows.["ColumDescripcion"].Value = a.descripcion;
            TablaDatos.Rows.["ColumPrecio"].Value = a.precio;
            TablaDatos.Rows.["ColumTipo"].Value = a.tipo;*/
            TablaDatos.Rows.Add(a.nombre, a.codigo, a.cantidad, a.descripcion, a.precio, a.tipo);
            
            
        }

        private void TablaDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
