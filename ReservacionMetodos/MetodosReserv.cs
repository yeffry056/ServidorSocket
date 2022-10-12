using ServidorConsola.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ServidorConsola.ReservacionMetodos
{
    public class MetodosReserv
    {
        public static void ListarReservacion()
        {
            var lista = ReservacionBLL.GetReservaciones();
            Console.WriteLine("======================================");
            Console.WriteLine("         Listado de reservaciones         ");
            Console.WriteLine("======================================\n");
            foreach (var item in lista)
            {
                var cliente = ClienteBLL.Buscar(item.ClienteId);
                var mesa = MesaBLL.Buscar(item.MesaId);
                Console.WriteLine("ReservacionId: " + item.reservacionId);
                Console.WriteLine("----------------");
                Console.WriteLine("ClinteId: " + item.ClienteId);
                Console.WriteLine("Nombres: " + cliente.nombres);
                Console.WriteLine("Telefono: " + cliente.Telefono);
                Console.WriteLine("----------------");
                Console.WriteLine("MesaId: " + item.MesaId);
                Console.WriteLine("Ubicacion: " + mesa.Ubicacion);
                Console.WriteLine("Forma: " + mesa.Forma);


                Console.WriteLine("\n\n");
            }
            Console.ReadKey();
        }

        public static void EliminarReserv()
        {
            Console.Clear();
            Console.WriteLine("======================================");
            Console.WriteLine("         Eliminar reservaciones         ");
            Console.WriteLine("======================================\n");

            Console.Write("Ingrese el Id: ");
            int id = int.Parse(Console.ReadLine());

            if (ReservacionBLL.Existe(id))
            {
                var reser = ReservacionBLL.Buscar(id);

                if (ReservacionBLL.Eliminar(id))
                {
                    Console.WriteLine("Reservacion eliminada.");

                    var m = MesaBLL.Buscar(reser.MesaId);
                    m.Dsiponibilidad = true;
                    MesaBLL.Guardar(m);
                    Thread.Sleep(1000);
                }
                else
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("No se pudo eliminar la reservacion");
                }
            }
            else
            {
                
                Console.WriteLine("La reservacion no existe.");
                Console.ReadKey();
            }
           
        }
    }
}
