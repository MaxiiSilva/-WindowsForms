using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Controller
{
    public class ProductoController
    {
        private List<Producto> Productos = new List<Producto>();
        private int ultimoID = 0;

        public void Agregar(String nombre, decimal precio, int stock){

            ultimoID++;
            Producto NuevoProducto = new Producto
            {
                id = ultimoID,
                Nombre = nombre,
                Precio = precio,
                Stock = stock

            };
            Productos.Add(NuevoProducto);

        }

        public List<Producto> ObtenerTodo(){ 
            return Productos;
        }
        
    }

}
