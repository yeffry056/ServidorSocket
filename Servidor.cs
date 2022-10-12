using ServidorConsola.entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServidorConsola.BLL;

namespace ServidorConsola
{
    public class Servidor
    {
        
        public static void server()
        {
            Reservacion reservacion;
            Cliente cliente;
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
                        data = Encoding.ASCII.GetString(bytes, 0, byteRec);

                        if (data.IndexOf("<EOF>") > -1)
                            break;
                    }

                    switch (Convert.ToInt32(data.Replace("<EOF>", "")))
                    {
                        case 1:
                            byte[] bytesM = null;
                            var LisMesa = MesaBLL.GetMesas();
                            byte[] can = Encoding.ASCII.GetBytes(Convert.ToString(LisMesa.Count) + "<EOF>");
                            handler.Send(can);

                            //enviando lista de mesas
                            Thread.Sleep(500);
                            foreach (var item in LisMesa)
                            {
                                bytesM = new byte[1024];

                                bytesM = Encoding.ASCII.GetBytes(Convert.ToString(item.MesaId) + "<EOF>");
                                handler.Send(bytesM);
                                Thread.Sleep(100);
                                bytesM = Encoding.ASCII.GetBytes(item.Ubicacion + "<EOF>");
                                handler.Send(bytesM);
                                Thread.Sleep(100);
                                bytesM = Encoding.ASCII.GetBytes(Convert.ToString(item.Capacidad) + "<EOF>");
                                handler.Send(bytesM);
                                Thread.Sleep(100);
                                bytesM = Encoding.ASCII.GetBytes(item.Forma + "<EOF>");
                                handler.Send(bytesM);
                                Thread.Sleep(100);
                                bytesM = Encoding.ASCII.GetBytes(Convert.ToString(item.Precio) + "<EOF>");
                                handler.Send(bytesM);
                                Thread.Sleep(100);
                                bytesM = Encoding.ASCII.GetBytes(Convert.ToString(item.Dsiponibilidad) + "<EOF>");
                                handler.Send(bytesM);

                            }
                            //
                            while (true)
                            {

                                bool aux = false, fin = false;
                                while (true)
                                {
                                    bytes = new byte[1024];
                                    int byteRec = handler.Receive(bytes);
                                    data = Encoding.ASCII.GetString(bytes, 0, byteRec);

                                    if (data.IndexOf("<EOF>") > -1)
                                        break;
                                }

                                if (Convert.ToInt32(data.Replace("<EOF>", "")) == 1)
                                {
                                    int reser = 1;

                                    reservacion = new Reservacion();
                                    while (true)
                                    {
                                        string dataRes = null;
                                        byte[] bytesRes = null;

                                        while (true)
                                        {
                                            bytesRes = new byte[1024];
                                            int byteRec = handler.Receive(bytesRes);
                                            dataRes += Encoding.ASCII.GetString(bytesRes, 0, byteRec);

                                            if (dataRes.IndexOf("<EOF>") > -1)
                                                break;

                                            if (Convert.ToInt32(dataRes) == 0)
                                            {

                                                aux = true;

                                                break;

                                            }


                                        }

                                        if (aux)
                                            break;

                                        if (reser == 1)
                                            reservacion.reservacionId = Convert.ToInt32(dataRes.Replace("<EOF>", ""));

                                        if (reser == 2)
                                            reservacion.ClienteId = Convert.ToInt32(dataRes.Replace("<EOF>", ""));

                                        if (reser == 3)
                                        {
                                            reservacion.MesaId = Convert.ToInt32(dataRes.Replace("<EOF>", ""));
                                            
                                           
                                            ReservacionBLL.Guardar(reservacion);
                                          
                                            foreach (var m in LisMesa)
                                            {
                                                if (m.MesaId == reservacion.MesaId)
                                                {
                                                    m.Dsiponibilidad = false;
                                                    MesaBLL.Guardar(m);
                                                }
                                            }
                                            fin = true;
                                            break;
                                        }

                                        reser++;



                                    }

                                    if (fin)
                                        break;
                                }
                                else
                                {
                                    if (Convert.ToInt32(data.Replace("<EOF>", "")) == 2)
                                    {
                                        break;
                                    }
                                }
                            }

                            break;
                        case 2:
                            int contP = 1;
                            cliente = new Cliente();
                            while (true)
                            {
                                string dataP = null;
                                byte[] bytesP = null;

                                while (true)
                                {
                                    bytesP = new byte[1024];
                                    int byteRec = handler.Receive(bytesP);
                                    dataP += Encoding.ASCII.GetString(bytesP, 0, byteRec);

                                    if (data.IndexOf("<EOF>") > -1)
                                        break;
                                }

                                if (contP == 1)
                                    cliente.ClienteId = Convert.ToInt32(dataP.Replace("<EOF>", ""));

                                if (contP == 2)
                                    cliente.nombres = dataP.Replace("<EOF>", "");

                                if (contP == 3)
                                    cliente.Telefono = dataP.Replace("<EOF>", "");

                                if (contP == 4)
                                {
                                    cliente.Email = dataP.Replace("<EOF>", "");
                                    break;
                                }


                                contP++;
                            }
                            ClienteBLL.Guardar(cliente);
                           
                            break;
                        case 3:
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("Opcion invalida...");
                            break;
                    }


                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }






    }
}
