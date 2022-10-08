using ServidorConsola;
using System.Net;
using System.Net.Sockets;
using System.Text;



internal class Program
{

    static Mesa mesa = new Mesa();
    static List<Mesa> LisMesa = new List<Mesa>();
    public static int cont = 1;
    public static void init()
    {
        // Mesa mesa = new Mesa();
       // List<Mesa> LisMesa = new List<Mesa>();
        mesa.MesaId = 1;
        mesa.Ubicacion = "frente a la playa";
        mesa.Capacidad = 4;
        mesa.Forma = "Redonda";
        mesa.Precio = 1500.00;
        mesa.Dsiponibilidad = true;
        LisMesa.Add(mesa);

       /* mesa.MesaId = 2;
        mesa.Ubicacion = "frente a la playa";
        mesa.Capacidad = 4;
        mesa.Forma = "Cuadrado";
        mesa.Precio = 1500.00;
        mesa.Dsiponibilidad = true;
        LisMesa.Add(mesa);*/



    }

    public static void Agregar()
    {
        int cant;
        Console.WriteLine("====================================");
        Console.WriteLine("        Agregando nuevas mesas         ");
        Console.WriteLine("====================================\n");
        Console.Write("Cuantas mesa desea agragar: ");
        cant = int.Parse(Console.ReadLine());   

        for(int i = 0; i < cant; i++)
        {
            Console.Write("\nMesaId: ");
            mesa.MesaId = int.Parse(Console.ReadLine());
            Console.Write("\nUbicacion: ");
            mesa.Ubicacion = Console.ReadLine();
            Console.Write("\nCapacidad: ");
            mesa.Capacidad = int.Parse(Console.ReadLine());
            Console.Write("\nForma: ");
            mesa.Forma = Console.ReadLine();
            Console.Write("\nPrecio: ");
            mesa.Precio = double.Parse(Console.ReadLine());

          
             LisMesa.Add(mesa);
            Console.Write("\n\n");

        }
        Console.Write("\nGuardado. ");
        Thread.Sleep(2000);
    }

    public static void Listar()
    {
        int i = 1;
        Console.WriteLine("==================================");
        Console.WriteLine("         Listado de Mesas         ");
        Console.WriteLine("==================================\n");
        foreach (var item in LisMesa)
        {
            
            Console.WriteLine("MesaId: " + item.MesaId);
            Console.WriteLine("Ubicacion: " + item.Ubicacion);
            Console.WriteLine("Capacidad: "+item.Capacidad);
            Console.WriteLine("Forma: "+item.Forma);
            Console.WriteLine("Precio: "+ item.Precio);
            Console.WriteLine("Disponibilidad: " + item.Dsiponibilidad);
            Console.WriteLine("\n\n");
        }
       Console.ReadKey();
       
    }
    public static void Listar2()
    {
        
        int i = 1;
        Console.WriteLine("==================================");
        Console.WriteLine("         Listado de Mesas         ");
        Console.WriteLine("==================================\n");
        for(int j = 0; j < LisMesa.Count; j++)
        {

            Console.WriteLine("MesaId: " + LisMesa[j].MesaId);
            Console.WriteLine("Ubicacion: " + LisMesa[j].Ubicacion);
           /* Console.WriteLine("Capacidad: " + item.Capacidad);
            Console.WriteLine("Forma: " + item.Forma);
            Console.WriteLine("Precio: " + item.Precio);
            Console.WriteLine("Disponibilidad: " + item.Dsiponibilidad);*/
            Console.WriteLine("\n\n");
        }
        Console.ReadKey();

    }
    private static void Main(string[] args)
    {

        int opc = 0;
        init();
        Thread hilo = new Thread(server);
        hilo.Start();
       
       
       

        
        do
        {
            Console.Clear();
            Console.Write("1.Listar Mesa\n2.Agregar Mesa\n3.Salir\nElija una opcion: ");
            opc = int.Parse(Console.ReadLine());

            switch (opc) { 
                case 1:
                    Console.Clear();
                    Listar2();
                    break;
                case 2:
                    Console.Clear();
                    Agregar();
                    break;
                case 3:
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Opcion invalida...");
                    break;

            
            }


        } while (opc != 3);
    }

    public static void server()
    {
        int op = 0;
        Mesa mesas;
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11200);

        try
        {
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(10);
            Console.WriteLine("Esperando Conexion");

            Socket handler = listener.Accept();

            while (true)
            {
                string data = null;
                byte[] bytes = null;

               /* mesas = new Mesa();
                foreach (var item in LisMesa)
                {
                    mesa.MesaId = item.MesaId;
                    mesa.Ubicacion = item.Ubicacion;
                    mesa.Capacidad = item.Capacidad;
                    mesa.Forma = item.Forma;
                    mesa.Dsiponibilidad = item.Dsiponibilidad;
                    mesa.Precio = item.Precio;
                    handler.Send(serialization.Serializate(mesa));
                }*/

                while (true)
                {
                    bytes = new byte[1024];
                    int byteRec = handler.Receive(bytes);
                    mesas = (Mesa) serialization.Deserializate(bytes);
                    LisMesa.Add(mesas);
                    // data += Encoding.ASCII.GetString(bytes, 0, byteRec);

                    if (data.IndexOf("<EOF>") > -1)
                        break;
                }
                Listar();

                Console.WriteLine("Texto del cliente: " + data.Replace("<EOF>", ""));

                //enviar mensaje de verificacion al cliente 
                //  byte[] msj = Encoding.Convert(mesa.Forma);
                byte[] msj = Encoding.ASCII.GetBytes("Recibido");
                handler.Send(msj);

                /*if ()
                    byte[] msj = Encoding.ASCII.GetBytes(LisMesa.);
                List<string> list = new List<string>();
                list = Encoding.ASCII.
                handler.Send(msj);*/
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }






}

/*
 
Mesa
-id
-ubicacion
-capacidad
-forma
-precio
-disponibilidad



1,frente a la playa,4,redonda,1500.00,disponible
1;frente a la playa;4;redonda;1500.00;disponible


byte[] recibido = new byte[1024];

if(socket.read(recibido) > 0)
s = new String(recibido);

String[] campos = s.split(",");


1|frente a la playa|4|redonda|1500.00|disponible


ID 4 bytes 
Descripcón 100 bytes
Capacidad 1 bytes
forma 20bytes
disponibilidad 20bytes

125 * 5 = 625



Los sistemas modernos se intercambian objetos 

XML
[<Object>
<ID>4</ID>
<Ubicacion>Frente a la plata</Ubicacion>
<Capacidad>4</Capacidad>
<Forma>Redonda</Forma>
<Precio>1500</Precio>
<Disponibilidad>Ocupada</Disponibilidad>
</Object>]

Json
[{
  "id":4,
  "ubicacion":"frente a la playa",
  "capacidad":4,
  "forma":"redonda",
  "precio":1500,
  "disponibilidad":"ocupada"
},
{},
{}]


.net java SprintBoot
$json = json_encode($modelo);
$modelo = json_decode($json);
 */
