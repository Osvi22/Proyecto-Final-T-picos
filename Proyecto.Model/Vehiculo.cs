using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Model
{
    [Table("Vehiculo")]
    public class Vehiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La placa es obligatoria")]
        // Limitamos la longitud a 6 exacta para ayudar a la validación
        [StringLength(6, MinimumLength = 6, ErrorMessage = "La placa debe tener exactamente 6 caracteres")]
        // Expresión Regular: 3 Letras Mayúsculas + 3 Números
        [RegularExpression(@"^[A-Z]{3}\d{3}$", ErrorMessage = "La placa debe tener el formato AAA000 (3 letras mayúsculas seguidas de 3 números)")]
        public string Placa { get; set; } = null!;

        [Required(ErrorMessage = "La marca es obligatoria")]
        [StringLength(100)]
        // Nueva validación: Solo letras (incluye tildes y ñ) y espacios
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$", ErrorMessage = "La marca solo puede contener letras")]
        public string Marca { get; set; } = null!;

        [Required(ErrorMessage = "El modelo es obligatorio")]
        [StringLength(100)]
        public string Modelo { get; set; } = null!;

        public int PersonaId { get; set; }
        public virtual Persona? Persona { get; set; }
    }

    // He añadido las mismas validaciones al DTO para proteger también la edición
    public class VehiculoEditarDto
    {
        [Required(ErrorMessage = "La placa es obligatoria")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "La placa debe tener exactamente 6 caracteres")]
        [RegularExpression(@"^[A-Z]{3}\d{3}$", ErrorMessage = "La placa debe tener el formato AAA000 (3 letras mayúsculas seguidas de 3 números)")]
        public string Placa { get; set; } = null!;

        [Required(ErrorMessage = "La marca es obligatoria")]
        [StringLength(100)]
        // Nueva validación también en el DTO
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$", ErrorMessage = "La marca solo puede contener letras")]
        public string Marca { get; set; } = null!;

        [Required(ErrorMessage = "El modelo es obligatorio")]
        [StringLength(100)]
        public string Modelo { get; set; } = null!;

        [Required]
        public int PersonaId { get; set; }
    }
}