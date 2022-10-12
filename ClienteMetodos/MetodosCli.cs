using ServidorConsola.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorConsola.ClienteMetodos
{
    public class MetodosCli
    {
        public static void listarCliente()
        {
            Console.Clear();
            var lista = ClienteBLL.GetClientes();
            Console.WriteLine("===================================");
            Console.WriteLine("         Listado de Clientes         ");
            Console.WriteLine("===================================\n");
            foreach (var item in lista)
            {

                Console.WriteLine("PersonaId: " + item.ClienteId);
                Console.WriteLine("Nombres: " + item.nombres);
                Console.WriteLine("Telefono: " + item.Telefono);
                Console.WriteLine("Email: " + item.Email);

                Console.WriteLine("\n\n");
            }
            Console.ReadKey();
        }
    }
}
