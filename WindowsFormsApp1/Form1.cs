using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Controller;
using System.Drawing.Text;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private ProductoController _Controller;
        private bool _Editar = false;
        private Producto _ProductoEditado = null;  
        public Form1()
        {

            InitializeComponent();
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            button4.Visible = false;
            _Controller = new ProductoController();
            


        }
        private void CargarProductos()
        {
            var lista = _Controller.ObtenerTodo();
            // Actualiza dataGridView2
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = lista;
            //Formato de numero entero  en la Columna
            dataGridView2.Columns["Stock"].DefaultCellStyle.Format= "N2";
            //Formato de Moneda en la Columna
            dataGridView2.Columns["Precio"].DefaultCellStyle.Format = "C2";
            label6.Text = $" Contador de Productos: {lista.Count}";
        }
        private Producto ObtenerSeleccionado()
        {
            if (dataGridView2.SelectedRows.Count == 0)

                return null;
                return dataGridView2.SelectedRows[0].DataBoundItem as Producto;
                var Producto = ObtenerSeleccionado();
                if (Producto == null) { return Producto; }
                
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                label3.Visible = true;
                textBox1.Focus();
                return;
            }
            else
            { label3.Visible = false; }
            if (!int.TryParse(textBox2.Text, out int stock) || stock <= 0)
            {
                label4.Visible = true;
                textBox2.Focus();
                return;
            }
            else 
            { label4.Visible = false; }
            if (!decimal.TryParse(textBox3.Text, out decimal precio) || precio <= 0)
            {
                label5.Visible = true;
                textBox3.Focus();
                return;
            }
            else { label5.Visible = false; }
           // _Controller.Agregar(nombre, stock, precio);
            if (_Editar)
            {
                _ProductoEditado.Nombre = textBox1.Text.Trim();
                _ProductoEditado.Stock = int.Parse(textBox2.Text);
                _ProductoEditado.Precio = decimal.Parse(textBox3.Text);
                _Controller.ModoEdicion(_ProductoEditado);
                SalirModoEdicion();
            }
            else
            {
                _Controller.Agregar(textBox1.Text.Trim(), int.Parse(textBox2.Text), decimal.Parse(textBox3.Text));
                CargarProductos();
                LimpiarCampos();
            }
            CargarProductos();
            LimpiarCampos();



        }
        private void LimpiarCampos() 
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Focus();
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

        private void button2_Click(object sender, EventArgs e)
        {
            var p = ObtenerSeleccionado();
            if (p == null) return;
            var Confirmar = MessageBox.Show($"Elimar {p.Nombre}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Confirmar == DialogResult.Yes)
            {
                _Controller.Eliminar(p.id);
                CargarProductos();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var p = ObtenerSeleccionado();
            if (p == null) return;
            _Editar = true;
            _ProductoEditado = p;   
            
            textBox1.Text = p.Nombre;
            textBox2.Text = p.Stock.ToString();
            textBox3.Text = p.Precio.ToString();
            ActualizarBoton();

        }
        private void ActualizarBoton() 
        {
            button1.Text = _Editar ? "Guardar Cambios" : "Agregar";
            button4.Visible = _Editar;    
            button3.Enabled = !_Editar;
        }

        private void SalirModoEdicion()
        { 
            _Editar = false;
            _ProductoEditado = null;
            ActualizarBoton();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SalirModoEdicion();
            LimpiarCampos();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Agarramos lo que escribió el usuario
            string texto = textBox1.Text.Trim();

            // Si no escribió nada, mostramos un aviso
            if (string.IsNullOrEmpty(texto))
            {
                MessageBox.Show("Escribí un producto para buscar.");
                textBox1.Focus();
                return;
            }

            // Buscamos los productos por nombre
            var lista = _Controller.ObtenerTodo()
                .Where(p => p.Nombre.ToLower().Contains(texto.ToLower()))
                .ToList();

            // Si no encontramos ningún producto
            if (lista.Count == 0)
            {
                MessageBox.Show("Producto no encontrado.");
                textBox1.Focus();
                return;
            }

            // Si encontramos productos, los mostramos
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = lista;
            
        }
        private void BuscarProductos()
        { 
        
        }
        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog(); //ese savefiledialog pregunta donde lo quiere guardar

            guardar.FileName = "productos.txt"; //el nombre del archivo

            guardar.Filter = "Archivo de texto (*.txt)|*.txt"; //permitimos guardar solo txt


            if (guardar.ShowDialog() == DialogResult.OK) //esto abre la ventana donde lo quiere guardar
            {
                var lista = _Controller.ObtenerTodo(); //la lista de produ

                string contenido = "LISTA DE PRODUCTOS\n\n"; //el conte del archivo

                foreach (Producto p in lista)
                {
                    contenido += $"ID: {p.id}\n"; contenido += $"Nombre: {p.Nombre}\n"; contenido += $"Stock: ${p.Stock}\n"; contenido += $"Precio: {p.Precio}\n\n";
                }

                // Guardamos el archivo
                File.WriteAllText(guardar.FileName, contenido);

                MessageBox.Show("Producto extraidos correctamente!!!"); //le avisame q termino
            }

        }
    }
}
