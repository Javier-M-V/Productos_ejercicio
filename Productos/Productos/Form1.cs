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
            TablaDatos.Rows.Add("D3200", 1, 55, 455.5 , "DX format", "Cuerpo",mod,del);//Fila de prueba
        }

        //inserto filas una a una
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
                if (comprobar(nombreValor, codigoValor))
                {
                    TablaDatos.Rows.Add(nombreValor, codigoValor, cantidadValor, precioValor, descripcionValor, tipoValor, mod, del);
                }
                else {

                    MessageBox.Show("Comprueba que el código no exista previamente y que el producto tenga nombre y código asignados");
                }
            }
        }

        //Compruebo que la clave no anda repetida
        private bool comprobar(string nombre, int codigo) {

            bool salida = true;
            for (int i = 0; i < TablaDatos.Rows.Count; i++) {

                DataGridViewRow row = TablaDatos.Rows[i];
                if ((Convert.ToInt32(row.Cells[1].Value)) == codigo)
                {
                    salida = false;
                }
                else if (nombre == "" || codigo == 0) {
                    salida = false;
                }
            }
            return salida;
        }

        //Exporto a CSV leyendo cada fila y escribiendo en fichero
        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                File.Create("productos_exportados.txt").Close();
                using (TextWriter tw = new StreamWriter("productos_exportados.txt", true))
                {
                    
                    for (int i = 0; i < TablaDatos.Rows.Count; i++)
                    {
                        DataGridViewRow row = TablaDatos.Rows[i];
                        tw.WriteLine(row.Cells[0].Value.ToString() + "," + row.Cells[1].Value.ToString() + "," + row.Cells[2].Value.ToString() + "," + row.Cells[4].Value.ToString() + "," + row.Cells[5].Value.ToString());
                    }
                    tw.Close();
                    MessageBox.Show("Exportado con éxito");
                }
            }
            catch (IOException) {

                MessageBox.Show("Error de lectura/escritura. Comprueba permisos y/o bloqueos."+ Environment.NewLine + "WTF ;)");
            }
   
        }

        //Importo a CSV,guardando cada string de cada línea en un array, previo split de las comas para luego subirlo a la tabla
        private void importarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TablaDatos.Rows.Count != 0)
            {
                char separadores = ',';
                string ruta = "";
                OpenFileDialog archivo = new OpenFileDialog();
                if (archivo.ShowDialog() == DialogResult.OK)
                {
                    ruta = archivo.FileName;
                }
                string[] lineastexto = System.IO.File.ReadAllLines(ruta);
                for (int i = 0; i < lineastexto.Length; i++)
                {

                    String[] valores = lineastexto[i].Split(separadores);
                    TablaDatos.Rows.Add(valores[0], valores[1], valores[2], valores[3], valores[4], valores[5], mod, del);
                }
            }
            else {
                MessageBox.Show("Nada que exportar. La tabla está vacía");
            }
        }

        //Operaciones sobre botones en la tabla
        private void TablaDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //borrado según índice
            string nombrearticulo = "";
            if (e.ColumnIndex == TablaDatos.Columns["ColumnBorrar"].Index)
            {
                nombrearticulo = TablaDatos.Rows[e.RowIndex].Cells[0].Value.ToString();
                DialogResult dialogResult = MessageBox.Show("¿Quieres borrar "+nombrearticulo+" ?", "Aviso", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    TablaDatos.Rows.RemoveAt(e.RowIndex);
                }
            }

            //Edición según ínidice  por medio de un formulario adaptado a la modificación
            if (e.ColumnIndex == TablaDatos.Columns["ColumnModificar"].Index)
            {
                nombrearticulo = TablaDatos.Rows[e.RowIndex].Cells[0].Value.ToString();
                modificacion a = new modificacion();
                a.Text = "Modificando "+nombrearticulo;
                DialogResult ventana = new DialogResult();
                ventana = a.ShowDialog();
                if (a.DialogResult == DialogResult.OK)
                {
                    if (nombreValor != "")
                    {
                        nombreValor = a.nombre;
                        cantidadValor = a.cantidad;
                        precioValor = a.precio;
                        descripcionValor = a.descripcion;
                        tipoValor = a.tipo;
                        TablaDatos.Rows[e.RowIndex].Cells[0].Value = nombreValor;
                        TablaDatos.Rows[e.RowIndex].Cells[2].Value = cantidadValor;
                        TablaDatos.Rows[e.RowIndex].Cells[3].Value = precioValor;
                        TablaDatos.Rows[e.RowIndex].Cells[4].Value = descripcionValor;
                        TablaDatos.Rows[e.RowIndex].Cells[5].Value = tipoValor;
                        TablaDatos.Rows[e.RowIndex].Cells[6].Value = mod;
                        TablaDatos.Rows[e.RowIndex].Cells[7].Value = del;
                        MessageBox.Show("Modificado");

                    }
                }
            }
        }
    }
}
