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

        public int PersonaId { get; set; }
        public virtual Persona Persona { get; set; } = null!;

    }

    public class VehiculoEditarDto
    {
        public string Placa { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int PersonaId { get; set; }
        
    }
  
}