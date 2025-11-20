using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Model
{
    [Table("Persona")]
    public class Persona
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La identificación es obligatoria")]
        [StringLength(50)]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [StringLength(100)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(50)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El salario es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser mayor o igual a 0")]
        public decimal Salario { get; set; }

        public virtual ICollection<Vehiculo>? Vehiculos { get; set; }
    }
}