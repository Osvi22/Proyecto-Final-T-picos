using Proyecto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BL
{
    public interface IPersonaRepository
    {
        
        
        Task AgregarAsync(Persona persona);
        Task<Persona?> ObtenerPorIdAsync(int id);


        Task<IEnumerable<Persona>> ObtenerAsync();
      //  Task ActualizarAsync(Persona persona);
      //  Task EliminarAsync(int id);


        Task<Persona?> ObtenerPorIdentificacionAsync(string identificacion);
        Task EditarPorIdentificacionAsync(string identificacion, Persona personaActualizada);
        Task EliminarPorIdentificacionAsync(string identificacion);

    }
}
