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
    public class MesaBLL
    {
        public static bool Guardar(Mesa mesa)
        {
            if (!Existe(mesa.MesaId))//si no existe insertamos
                return Insertar(mesa);
            else
                return Modificar(mesa);
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Mesa.Any(e => e.MesaId == id);
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
                var mesa = contexto.Mesa.Find(id);

                if (mesa != null)
                {


                    contexto.Mesa.Remove(mesa); //remover la entidad
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
        private static bool Insertar(Mesa mesa)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {

                if (contexto.Mesa.Add(mesa) != null)
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

        private static bool Modificar(Mesa mesa)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {

                contexto.Entry(mesa).State = EntityState.Modified;
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
        public static List<Mesa> GetMesas()
        {
            List<Mesa> Lista = new List<Mesa>();
            Contexto contexto = new Contexto();

            try
            {

                Lista = contexto.Mesa.ToList();
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

        public static Mesa Buscar(int id)
        {
            Mesa mesa;
            Contexto contexto = new Contexto();

            try
            {
                mesa = contexto.Mesa.Find(id);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return mesa;
        }
    }
}

