namespace Proyecto.Model
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }

        // Clave foránea para la relación con Persona
        public int PersonaId { get; set; }

        // Propiedad de navegación para el dueño
        public virtual Persona? Dueno { get; set; }
    }
}