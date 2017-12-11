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
    public partial class Exportar : Form
    {
        public string nombrearchivo { get; set; }

        public Exportar()
        {
            InitializeComponent();
        }

        //cerramos el form
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //guardamos el nombre
        private void button1_Click(object sender, EventArgs e)
        {
            nombrearchivo = textBoxExportar.Text;
            this.Close();
        }
    }
}
