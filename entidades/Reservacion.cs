using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorConsola.entidades
{
    public class Reservacion
    {
        [Key]
        public int reservacionId { get; set; }
        public int ClienteId { get; set; }
        public int MesaId { get; set; }


        
   
    }
}
