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
    public partial class Form2 : Form
    {
        public string nombre { get; set; }
        public int codigo { get; set; }
        public int cantidad { get; set; }
        public string descripcion { get; set; }
        public double precio { get; set; }
        public string tipo { get; set; }

        public Form2()
        {
            InitializeComponent();

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
    }
}
