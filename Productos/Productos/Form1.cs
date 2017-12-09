using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Productos
{
     
    public partial class Form1 : Form
    {
        public string nombreValor = "";
        public int codigoValor = 0;
        public int cantidadValor = 0;
        public double precioValor = 0.0;
        public string descripcionValor = "";
        public string tipoValor = "";/*Lentes,Cuerpos,Accesorios,Herrajes,Fundas y transporte*/
        public string mod = "✍";
        public string del = "✗";

        public Form1()
        {
            InitializeComponent();
            this.Text = "Productos";
            TablaDatos.Rows.Add("D3200", 1, 55, 455.5 , "DX format", "Cuerpo",mod,del);
        }

        private void insertarNuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            a.Text = "Nuevo producto";
            DialogResult ventana = new DialogResult();
            ventana = a.ShowDialog();
            if (a.DialogResult == DialogResult.OK)
            {
                nombreValor = a.nombre;
                codigoValor = a.codigo;
                cantidadValor = a.cantidad;
                precioValor = a.precio;
                descripcionValor = a.descripcion;
                tipoValor = a.tipo;
                if (comprobar())
                {
                    TablaDatos.Rows.Add(nombreValor, codigoValor, cantidadValor, precioValor, descripcionValor, tipoValor, mod, del);
                }
                else {

                    MessageBox.Show("Hay campos clave repetidos. Comprueba que el código no exista y que el producto tenga nombre");
                }
            }
        }
        private bool comprobar() {

            return true;
        }


        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            /*File.Create("productos_exportados.txt");
            TextWriter tw = new StreamWriter("productos_exportados.txt", true);//FIX THIS
            for (int i = 0; i < TablaDatos.Rows.Count; i++) {

                DataGridViewRow row = TablaDatos.Rows[i];
                tw.WriteLine(row.Cells[1].Value.ToString() + row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[5].Value.ToString() +mod+del);

            }
            tw.Close();*/
        }

        private void importarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            char seps = ',';
            string ruta="";
            OpenFileDialog archivo = new OpenFileDialog();
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                ruta = archivo.FileName;
            }
            string[] lineastexto = System.IO.File.ReadAllLines(ruta);
            for(int i=0;i<lineastexto.Length; i++)
            {
                
                String[] valores = lineastexto[i].Split(seps);
                TablaDatos.Rows.Add(valores[0], valores[1], valores[2], valores[3], valores[4], valores[5],mod, del);
            }
        }
    }
}
