using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proyecto.Model;

namespace Proyecto.DA
{
    public class DBContexto : DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> options) : base(options)
        {
        }



        public DbSet<Persona> Personas { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Vehiculo>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.Persona)
                .WithMany() // No hay ICollection<Vehiculo> en Persona
                .HasForeignKey(v => v.PersonaId);
        }
    }
}