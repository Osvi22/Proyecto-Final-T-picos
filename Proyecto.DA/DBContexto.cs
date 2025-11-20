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
            // Establecemos las claves primarias (buena práctica)
            modelBuilder.Entity<Persona>().HasKey(p => p.Id);
            modelBuilder.Entity<Vehiculo>().HasKey(v => v.Id);

            // --- CORRECCIÓN CRÍTICA DE LA RELACIÓN ---
            // 1. Configurar la relación desde Vehiculo (el lado "Muchos")
            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.Persona) // Un Vehiculo tiene UNA Persona (dueño)
                .WithMany(p => p.Vehiculos) // Y esa Persona tiene MUCHOS Vehiculos (la propiedad que agregamos a Persona.cs)
                .HasForeignKey(v => v.PersonaId) // La FK es PersonaId
                .OnDelete(DeleteBehavior.Restrict); // Evita borrados en cascada no deseados

            base.OnModelCreating(modelBuilder);
        }
    }
}