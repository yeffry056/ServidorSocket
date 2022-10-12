using Microsoft.EntityFrameworkCore;
using ServidorConsola.DAL;
using ServidorConsola.entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorConsola.BLL
{
    public class ReservacionBLL
    {
        public static bool Guardar(Reservacion reservacion)
        {
            if (!Existe(reservacion.reservacionId))//si no existe insertamos
                return Insertar(reservacion);
            else
                return Modificar(reservacion);
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Reservacion.Any(e => e.reservacionId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //buscar la entidad que se desea eliminar
                var reservacion = contexto.Reservacion.Find(id);

                if (reservacion != null)
                {


                    contexto.Reservacion.Remove(reservacion); //remover la entidad
                    paso = contexto.SaveChanges() > 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static Reservacion Buscar(int id)
        {
            Reservacion reservacion;
            Contexto contexto = new Contexto();

            try
            {
                reservacion = contexto.Reservacion.Find(id);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return reservacion;
        }
        private static bool Insertar(Reservacion reservacion)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                
                if (contexto.Reservacion.Add(reservacion) != null)
                    paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        private static bool Modificar(Reservacion reservacion)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {

                contexto.Entry(reservacion).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static List<Reservacion> GetReservaciones()
        {
            List<Reservacion> Lista = new List<Reservacion>();
            Contexto contexto = new Contexto();

            try
            {

                Lista = contexto.Reservacion.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
    }
}
