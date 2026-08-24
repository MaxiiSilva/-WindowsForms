using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class Producto : IEntidad
    {
        public int id{ get; set; }
        public string Nombre { get; set; }
        public decimal Precio{ get; set; }
        public int Stock { get; set; }
    }
}
