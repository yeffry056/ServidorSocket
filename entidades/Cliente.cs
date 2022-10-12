using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorConsola.entidades
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string nombres { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
