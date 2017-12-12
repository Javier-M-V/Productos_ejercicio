using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


/*He metido los form para cada operación, pero para importar, lo más conveniente
 es lo más común: usar un OpenFileDialog */

namespace Productos
{
    public partial class Principal : Form
    {
        public string nombreValor = "";
        public int codigoValor = 0;
        public int cantidadValor = 0;
        public double precioValor = 0.0;
        public string descripcionValor = "";
        public string tipoValor = "";/*Lentes, Cuerpos, Accesorios, Herrajes, Fundas y transporte*/
        public string mod = "✍";
        public string del = "✗";

        public Principal()
        {
            InitializeComponent();
            this.Text = "Productos";
            Image imagen = Image.FromFile(@".\d3200.jpeg");
            TablaDatos.Rows.Add("D3200", 1, 55, 455.5, "DX format", "Cuerpo", imagen, mod, del);//Fila de prueba
        }

        //inserto filas una a una desde el Form2
        private void insertarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Alta a = new Alta();
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
                    TablaDatos.Rows.Add(nombreValor, codigoValor, cantidadValor, precioValor, descripcionValor, tipoValor, null, mod, del);
                }
                else
                {

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
                Exportar a = new Exportar();
                a.Text = "Ruta a exportar";
                DialogResult ventana = new DialogResult();
                ventana = a.ShowDialog();
                if (a.DialogResult == DialogResult.OK)
                {
                    File.Create(a.nombrearchivo + ".txt").Close();
                    using (TextWriter tw = new StreamWriter(a.nombrearchivo + ".txt", true))
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
            }
            catch (IOException) {

                MessageBox.Show("Error de lectura/escritura. Comprueba permisos y/o bloqueos." + Environment.NewLine + "WTF ;)");
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
                    string[] lineastexto = System.IO.File.ReadAllLines(ruta);
                    for (int i = 0; i < lineastexto.Length; i++)
                    {

                        String[] valores = lineastexto[i].Split(separadores);
                        TablaDatos.Rows.Add(valores[0], Int32.Parse(valores[1]), Int32.Parse(valores[2]), Double.Parse(valores[3]), valores[4], valores[5], null, mod, del);
                    }
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
                DialogResult dialogResult = MessageBox.Show("¿Quieres borrar " + nombrearticulo + " ?", "Aviso", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    TablaDatos.Rows.RemoveAt(e.RowIndex);
                }
            }

            //Edición según ínidice  por medio de un formulario adaptado a la modificación
            if (e.ColumnIndex == TablaDatos.Columns["ColumnModificar"].Index)
            {
                nombrearticulo = TablaDatos.Rows[e.RowIndex].Cells[0].Value.ToString();
                Modificacion a = new Modificacion();
                a.Text = "Modificando " + nombrearticulo;
                DialogResult ventana = new DialogResult();
                ventana = a.ShowDialog();
                if (a.DialogResult == DialogResult.OK)
                {
                    if (a.nombre != "")
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
                        TablaDatos.Rows[e.RowIndex].Cells[6].Value = null;
                        TablaDatos.Rows[e.RowIndex].Cells[7].Value = mod;
                        TablaDatos.Rows[e.RowIndex].Cells[8].Value = del;
                        MessageBox.Show("Modificado");
                    }
                }
            }
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e, DataGridViewCellEventArgs ev)
        {

        }

        private void modificacion()
        {
            string nombrearticulo = TablaDatos.SelectedRows[0].Cells[1].Value.ToString();
            int index = TablaDatos.SelectedRows[0].Index;
            Modificacion a = new Modificacion();
            a.Text = "Modificando " + nombrearticulo;
            DialogResult ventana = new DialogResult();
            ventana = a.ShowDialog();
            if (a.DialogResult == DialogResult.OK)
            {
                nombreValor = a.nombre;
                cantidadValor = a.cantidad;
                precioValor = a.precio;
                descripcionValor = a.descripcion;
                tipoValor = a.tipo;
                TablaDatos.Rows[index].Cells[0].Value = nombreValor;
                TablaDatos.Rows[index].Cells[2].Value = cantidadValor;
                TablaDatos.Rows[index].Cells[3].Value = precioValor;
                TablaDatos.Rows[index].Cells[4].Value = descripcionValor;
                TablaDatos.Rows[index].Cells[5].Value = tipoValor;
                TablaDatos.Rows[index].Cells[6].Value = null;
                TablaDatos.Rows[index].Cells[7].Value = mod;
                TablaDatos.Rows[index].Cells[8].Value = del;
                MessageBox.Show("Modificado");
            }
        }

        //modificación a partir de selección 
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificacion();
        }


        private void buttonModificar_Click(object sender, EventArgs e)
        {
            modificacion();
        }


        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            borrado();
        }

        private void borrado()
        {

            string nombrearticulo = "";
            int indice = 0;
            int numerofilas = TablaDatos.Rows.Count;
            for (int a = 0; a < numerofilas; a++)
            {
                if (TablaDatos.Rows[0].Selected) {

                    nombrearticulo = TablaDatos.Rows[0].Cells[0].Value.ToString();
                    indice = TablaDatos.Rows[0].Index;
                }
                DialogResult dialogResult = MessageBox.Show("¿Quieres borrar " + nombrearticulo + " ?", "Aviso", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    TablaDatos.Rows.RemoveAt(indice);
                }

            }
        }

        private void buttonBorrar_Click_1(object sender, EventArgs e)
        {
            borrado();
        }
    }
}
      
    