namespace Proyecto.Model
{
    public class Persona
    {
        public int Id { get; set; }
        public string? Identificacion { get; set; } // Identificación única (cédula, DNI, etc.)
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public decimal Salario { get; set; }

        // Propiedad de navegación (opcional pero recomendada)
        public virtual ICollection<Vehiculo>? Vehiculos { get; set; }
    }
}