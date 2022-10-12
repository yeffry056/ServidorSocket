using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServidorConsola.entidades;

namespace ServidorConsola.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Mesa> Mesa { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Reservacion> Reservacion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = DATA\Servidor.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mesa>().HasData(new Mesa
            {
                MesaId = 1,
                Ubicacion = "frente a la playa",
                Capacidad = 4,
                Forma = "Redonda",
                Precio = 1500.00,
                Dsiponibilidad = true
        });

            modelBuilder.Entity<Mesa>().HasData(new Mesa
            {
                MesaId = 2,
                Ubicacion = "frente a la playa",
                Capacidad = 4,
                Forma = "Cuadrado",
                Precio = 1500.00,
                Dsiponibilidad = true
        });

           
        }
    }
}
