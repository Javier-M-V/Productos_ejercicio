﻿using System;
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
        public string nombreValor { get; set; }
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
            this.Text = "Productos";
            TablaDatos.Rows.Add("D3200", 1, 55, "DX format", 455.5, "Cuerpo",mod,del);
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
                descripcionValor = a.descripcion;
                precioValor = a.precio;
                tipoValor = a.tipo;
                TablaDatos.Rows.Add(nombreValor, codigoValor, cantidadValor, descripcionValor, precioValor, tipoValor, mod, del);

            }
        }


        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void importarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
