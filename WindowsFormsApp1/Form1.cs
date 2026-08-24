using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Controller;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private ProductoController _Controller;
        public Form1()
        {
            InitializeComponent();
            _Controller = new ProductoController();

        }
        private void CargarProductos()
        {
            // Asume que tu control en la interfaz se llama dataGridView1
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = _Controller.ObtenerTodo();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            decimal precio = decimal.Parse(textBox2.Text);
            int stock = int.Parse(textBox3.Text);
            _Controller.Agregar(nombre, precio, stock);
            CargarProductos();
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
                
        }
    }
}
