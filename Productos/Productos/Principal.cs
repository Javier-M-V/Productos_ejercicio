using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


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
        public string rutaimagen = "";

        public Principal()
        {
            InitializeComponent();
            this.Text = "Productos";
            Image imagen = Image.FromFile(@".\d3200.jpg");
            TablaDatos.Rows.Add("D3200", 1, 55, 455.5, "DX format", "Cuerpo", imagen, mod, del);//Fila de prueba
        }

        //inserto filas una a una desde el Form2
        private void insertarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anadir();
            comprobacion();
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

        //Export a CSV leyendo cada fila y escribiendo en fichero
        private void Exportar_AToolStripMenuItem_Click(object sender, EventArgs e)
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
                comprobacion();
            }
            catch (IOException) {

                MessageBox.Show("Error de lectura/escritura. Comprueba permisos y/o bloqueos." + Environment.NewLine + "WTF ;)");
            }
        }

        //importo a CSV,guardando cada string de cada línea en un array, previo split de las comas para luego subirlo a la tabla
        private void Importar_AToolStripMenuItem_Click(object sender, EventArgs e)
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
                comprobacion();
            }

            else {
                MessageBox.Show("Nada que exportar. La tabla está vacía");
                comprobacion();
            }
        }

        //Operaciones sobre BOTONES en la tabla
        private void TablaDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //borrado según índice por medio de botón
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
            comprobacion();

            //Edición según ínidice  por medio de un formulario adaptado a la modificación por medio de botón
            if (e.ColumnIndex == TablaDatos.Columns["ColumnModificar"].Index)
            {
                Image imagen = null;
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
                        rutaimagen = a.ruta;
                        try
                        {
                            imagen = Image.FromFile(rutaimagen);
                            MessageBox.Show(rutaimagen);
                        }
                        catch (Exception) { MessageBox.Show("Explota la imagen"); imagen = null; }
                        TablaDatos.Rows[e.RowIndex].Cells[0].Value = nombreValor;
                        TablaDatos.Rows[e.RowIndex].Cells[2].Value = cantidadValor;
                        TablaDatos.Rows[e.RowIndex].Cells[3].Value = precioValor;
                        TablaDatos.Rows[e.RowIndex].Cells[4].Value = descripcionValor;
                        TablaDatos.Rows[e.RowIndex].Cells[5].Value = tipoValor;
                        TablaDatos.Rows[e.RowIndex].Cells[6].Value = imagen;
                        TablaDatos.Rows[e.RowIndex].Cells[7].Value = mod;
                        TablaDatos.Rows[e.RowIndex].Cells[8].Value = del;
                        MessageBox.Show("Modificado");
                    }
                }
            }
        }

        //boton añadir de abajo
        private void buttonAnadir_Click(object sender, EventArgs e)
        {
            Anadir();
            comprobacion();
        }

        //botón de modificación a partir de selección 
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificacion();
        }

        //botón de modificación
        private void buttonModificar_Click(object sender, EventArgs e)
        {
            modificacion();
        }

        //botón de borrado a partir de selección
        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            borrado();
            comprobacion();
        }

        //botón de borrado a partir de selección
        private void buttonBorrar_Click_1(object sender, EventArgs e)
        {
            borrado();
            comprobacion();
        }

        //función de borrado, que realiza un bucle a partir de toda la selección, preguntado al usuario si quiere borrar
        private void borrado()
        {
            string nombrearticulo = "";
            int indice = 0;
            int numerofilas = TablaDatos.Rows.Count;
            int seleccionadas = TablaDatos.Rows.GetRowCount(DataGridViewElementStates.Selected);

            for (int i=0; i< seleccionadas; i++)//todo esto porque no quiero borrar al revés y quiero preguntar producto por producto
            {
                for (int a = 0; a < TablaDatos.Rows.Count; a++)
                {
                    if (TablaDatos.Rows[a].Selected)
                    {
                        nombrearticulo = TablaDatos.Rows[a].Cells[0].Value.ToString();
                        indice = TablaDatos.Rows[a].Index;
                        DialogResult dialogResult = MessageBox.Show("¿Quieres borrar " + nombrearticulo + " ?", "Aviso", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            TablaDatos.Rows.RemoveAt(indice);
                        }
                    }
                }
            }
            comprobacion();
        }

        //funcion de modificación a partir de selección
        private void modificacion()
        {
            try
            {
                Image imagen = null;
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
                    rutaimagen = a.ruta;
                    try
                    {
                        imagen = Image.FromFile(rutaimagen);
                        MessageBox.Show(rutaimagen);
                    }
                    catch (Exception) { MessageBox.Show("Explota la imagen"); imagen = null; }
                    TablaDatos.Rows[index].Cells[0].Value = nombreValor;
                    TablaDatos.Rows[index].Cells[2].Value = cantidadValor;
                    TablaDatos.Rows[index].Cells[3].Value = precioValor;
                    TablaDatos.Rows[index].Cells[4].Value = descripcionValor;
                    TablaDatos.Rows[index].Cells[5].Value = tipoValor;
                    TablaDatos.Rows[index].Cells[6].Value = imagen;
                    TablaDatos.Rows[index].Cells[7].Value = mod;
                    TablaDatos.Rows[index].Cells[8].Value = del;
                    MessageBox.Show("Modificado");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Ninguna fila seleccionada");
            }
        }

        //Añadir productos 
        private void Anadir()
        {
            Image imagen = null;
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
                rutaimagen = a.ruta;
                try
                {
                    imagen = Image.FromFile(rutaimagen);
                }
                catch (Exception) { imagen = null; }

                if (comprobar(nombreValor, codigoValor))
                {
                    TablaDatos.Rows.Add(nombreValor, codigoValor, cantidadValor, precioValor, descripcionValor, tipoValor, imagen, mod, del);
                }
                else
                {
                    MessageBox.Show("Comprueba que el código no exista previamente y que el producto tenga nombre y código asignados");
                }
            }
        }

        //función que comprueba el estado de la tabla para activar o desactivar botones
        private void comprobacion()
        {
            if (TablaDatos.Rows.Count > 0)
            {

                exportarToolStripMenuItem.Enabled = true;
                importarToolStripMenuItem.Enabled = true;
                modificarToolStripMenuItem.Enabled = true;
                borrarToolStripMenuItem.Enabled = true;
                buttonBorrar.Enabled = true;
                buttonModificar.Enabled = true;
            }
            else
            {
                exportarToolStripMenuItem.Enabled = false;
                importarToolStripMenuItem.Enabled = false;
                modificarToolStripMenuItem.Enabled = false;
                borrarToolStripMenuItem.Enabled = false;
                buttonBorrar.Enabled = false;
                buttonModificar.Enabled = false;
            }
        }
    }
}
      
    