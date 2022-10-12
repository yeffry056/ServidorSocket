using ServidorConsola;
using ServidorConsola.BLL;
using ServidorConsola.ClienteMetodos;
using ServidorConsola.entidades;
using ServidorConsola.ReservacionMetodos;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Xceed.Wpf.Toolkit;

namespace ServidorConsola
{
    public class Program
    {
        
        static Mesa mesa;
        
       
        public static void Agregar()
        {
            int cant, aux = 0;
            Console.WriteLine("====================================");
            Console.WriteLine("        Agregando nuevas mesas         ");
            Console.WriteLine("====================================\n");
            Console.Write("Cuantas mesa desea agragar: ");
            cant = int.Parse(Console.ReadLine());
            var M = MesaBLL.GetMesas();
            aux = M.Count;
            for (int i = 0; i < cant; i++)
            {
                aux++;
                mesa = new Mesa();
                
                mesa.MesaId = aux; 
                Console.Write("\nUbicacion: ");
                mesa.Ubicacion = Console.ReadLine();
                Console.Write("\nCapacidad: ");
                mesa.Capacidad = int.Parse(Console.ReadLine());
                Console.Write("\nForma: ");
                mesa.Forma = Console.ReadLine();
                Console.Write("\nPrecio: ");
                mesa.Precio = double.Parse(Console.ReadLine());
                mesa.Dsiponibilidad = true;


                if (!validarMesa(mesa))
                {
                    Console.WriteLine("No se pudo guardar devido a compos erroneos.");
                    Thread.Sleep(1500);
                    break;
                }
                else
                {
                    var paso = MesaBLL.Guardar(mesa);
                    Console.Write("\n\n");
                    Console.Write("\nGuardado. ");
                    Thread.Sleep(1000);

                }
              

            }
           
        }
        public static bool validarMesa(Mesa mesa)
        {
            bool esValido = true;
            if (mesa.Ubicacion.Length == 0)
            {
                esValido = false;
                Console.WriteLine("Ubicacion vacia");
                return esValido;
            }
            if (mesa.Capacidad <= 0)
            {
                esValido = false;
                Console.WriteLine("Capacidad incorrecta");
                return esValido;
            }
            if (mesa.Forma.Length == 0)
            {
                esValido = false;
                Console.WriteLine("Forma vacia");
                return esValido;
            }
            if (mesa.Precio <= 0)
            {
                esValido = false;
                Console.WriteLine("Precio incorrecto");
                return esValido;
            }
            return esValido;
        }
        public static void Listar()
        {
            var lista = MesaBLL.GetMesas();

            Console.WriteLine("==================================");
            Console.WriteLine("         Listado de Mesas         ");
            Console.WriteLine("==================================\n");

            if(lista.Count == 0)
            {
                Console.WriteLine("Lista vacia");
                Thread.Sleep(1000);
                return;
            }
            else
            {
                foreach (var item in lista)
                {

                    Console.WriteLine("MesaId: " + item.MesaId);
                    Console.WriteLine("Ubicacion: " + item.Ubicacion);
                    Console.WriteLine("Capacidad: " + item.Capacidad);
                    Console.WriteLine("Forma: " + item.Forma);
                    Console.WriteLine("Precio: " + item.Precio);
                    Console.WriteLine("Disponibilidad: " + item.Dsiponibilidad);
                    Console.WriteLine("\n\n");
                }
                Console.ReadKey();
            }
           

        }
        private static void Main(string[] args)
        {

            int opc = 0;
            
            Thread hilo = new Thread(Servidor.server);
            hilo.Start();

            do
            {
                Console.Clear();
                Console.Write("1.Listar Mesa\n2.Agregar Mesa\n3.Listar Clientes\n4.Listar Reservaciones\n5.Eliminar Reservacion\n6.Salir\nElija una opcion: ");
                opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        Listar();
                        break;
                    case 2:
                        Console.Clear();
                        Agregar();
                        break;
                    case 3:
                        MetodosCli.listarCliente(); 

                        break;
                    case 4:
                        Console.Clear();
                        MetodosReserv.ListarReservacion();

                        break;

                    case 5:

                        MetodosReserv.EliminarReserv();

                        break;

                    case 6:
                        Console.Clear();
                        Environment.Exit(0);
                            break;
                    default:
                        Console.WriteLine("\nOpcion invalida...");
                        Thread.Sleep(1000);
                        break;


                }


            } while (opc != 6);
        }

    }
}


