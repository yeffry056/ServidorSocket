using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorConsola.entidades
{
   
    public class Mesa
    {
        [Key]
        public int MesaId { get; set; }
        public string Ubicacion { get; set; }
        public int Capacidad { get; set; }
        public string Forma { get; set; }
        public double Precio { get; set; }
        public bool Dsiponibilidad { get; set; }
    }
}
