using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Model;

namespace Proyecto.UI.Models
{
    /// <summary>
    /// Este ViewModel transporta todos los datos necesarios
    /// para la vista "Editar Propietario".
    /// </summary>
    public class PropietarioEditViewModel
    {
        public Vehiculo Vehiculo { get; set; }

        public SelectList ListaDePersonas { get; set; }

        public int PersonaIdSeleccionada { get; set; }
    }
}