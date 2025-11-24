using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Model
{
    [Table("Persona")]
    public class Persona
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La identificación es obligatoria")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "La identificación debe tener exactamente 9 dígitos")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "La identificación solo debe contener números")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$", ErrorMessage = "El nombre solo puede contener letras")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$", ErrorMessage = "Los apellidos solo pueden contener letras")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(50)]
        [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "El formato del teléfono debe ser 0000-0000")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El salario es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser mayor o igual a 0")]
        public decimal Salario { get; set; }

        public virtual ICollection<Vehiculo>? Vehiculos { get; set; }
    }
}