using Proyecto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BL
{
    public interface IAdministradorDePersonas
    {
        
        Task AgregueAsync(Persona persona);
        Task<IEnumerable<Persona>> ObtengaLaListaAsync();
        Task<Persona?> ObtengaLaPersonaAsync(int id);

        Task<Persona?> ObtengaPorIdentificacionAsync(string identificacion);
        Task EditePorIdentificacionAsync(string identificacion, Persona persona);
        Task EliminePorIdentificacionAsync(string identificacion);
    }
}
