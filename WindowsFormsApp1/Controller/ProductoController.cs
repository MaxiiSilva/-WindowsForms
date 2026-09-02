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

        public void Agregar(String nombre, int stock, decimal precio)
        {

            ultimoID++;
            Producto NuevoProducto = new Producto
            {
                id = ultimoID,
                Nombre = nombre,
                Stock = stock,
                Precio = precio

            };
            Productos.Add(NuevoProducto);

        }

        public List<Producto> ObtenerTodo() {
            return Productos;
        }
        public void Eliminar(int id) 
        {
            Productos.RemoveAll(p => p.id == id);
        }

        public void ModoEdicion( Producto Modificado)
        {
            var p = Productos.Find(x => x.id == Modificado.id);
            if (p == null) return;
            p.Nombre = Modificado.Nombre;   
            p.Stock = Modificado.Stock;
            p.Precio = Modificado.Precio;  
        }
        
    }

}
