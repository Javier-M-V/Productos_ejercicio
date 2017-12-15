using System;
using System.Windows.Forms;

namespace Productos
{
    public partial class Alta : Form
    {
        //Properties para pasar a principal
        public string nombre { get; set; }
        public int codigo { get; set; }
        public int cantidad { get; set; }
        public string descripcion { get; set; }
        public double precio { get; set; }
        public string tipo { get; set; }
        public string ruta { get; set; }

        public Alta()
        {
            InitializeComponent();

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Compruebo las claves y cargo los valores si no hay errores
        private void buttonAlta_Click(object sender, EventArgs e)
        {
            if(textBoxNombre.Text=="" || numericUpDownCodigo.Value.Equals(""))
            {
                MessageBox.Show("Hay campos clave vacíos"); 
            }
            else
            {
                nombre = textBoxNombre.Text;
                codigo = Convert.ToInt32(numericUpDownCodigo.Value);
                cantidad = Convert.ToInt32(numericUpDownCantidad.Value);
                descripcion = textBoxDescripcion.Text;
                precio = Convert.ToDouble(numericUpDownPrecio.Value);
                tipo = ComboBoxTipo.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void buttonRuta_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                ruta = archivo.FileName;
                textBoxImagen.Text = ruta;
            }
        }
    }
}
