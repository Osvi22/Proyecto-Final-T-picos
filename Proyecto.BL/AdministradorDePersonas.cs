using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Model;

namespace Proyecto.BL
{
    public class AdministradorDePersonas : IAdministradorDePersonas
    {
        private readonly IPersonaRepository _personaRepository;

        public AdministradorDePersonas(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;

        }



        public async Task AgregueAsync(Persona persona)
        {
            await _personaRepository.AgregarAsync(persona);
        }


        public async Task<IEnumerable<Persona>> ObtengaLaListaAsync()
        {
            return await _personaRepository.ObtenerAsync();
        }


        public async Task<Persona?> ObtengaLaPersonaAsync(int id)
        {
            return await _personaRepository.ObtenerPorIdAsync(id);
        }



               public async Task<Persona?> ObtengaPorIdentificacionAsync(string identificacion)
        {
            return await _personaRepository.ObtenerPorIdentificacionAsync(identificacion);
        }

        public async Task EditePorIdentificacionAsync(string identificacion, Persona persona)
        {
            await _personaRepository.EditarPorIdentificacionAsync(identificacion, persona);
        }

        public async Task EliminePorIdentificacionAsync(string identificacion)
        {
            await _personaRepository.EliminarPorIdentificacionAsync(identificacion);
        }


    }
}
