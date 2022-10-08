using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServidorConsola
{
    public class servidor
    {

        public static void server()
        {
            int op = 0;
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

                    while (true)
                    {
                        bytes = new byte[1024];
                        int byteRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, byteRec);

                        if (data.IndexOf("<EOF>") > -1)
                            break;
                    }

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
}
