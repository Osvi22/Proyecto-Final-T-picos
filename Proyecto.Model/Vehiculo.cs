using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Model

{
    [Table("Vehiculo")]
    public class Vehiculo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La placa es obligatoria")]
        [StringLength(50)]
        public string Placa { get; set; } = null!;

        [Required(ErrorMessage = "La marca es obligatoria")]
        [StringLength(100)]
        public string Marca { get; set; } = null!;

        [Required(ErrorMessage = "El modelo es obligatorio")]
        [StringLength(100)]
        public string Modelo { get; set; } = null!;
        // Clave foránea para la relación con Persona


        public int PersonaId { get; set; }
        public Persona Persona { get; set; } = null!;

        //public string PersonaIdentificacion { get; set; } = null!;

    }

    // DTO para edición
    public class VehiculoEditarDto
    {
        public string Placa { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int PersonaId { get; set; }
        
    }

    
}