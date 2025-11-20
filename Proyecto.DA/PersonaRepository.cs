using Microsoft.EntityFrameworkCore;
using Proyecto.BL;
using Proyecto.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.DA
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly DBContexto _context;

        public PersonaRepository(DBContexto context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Persona>> ObtenerAsync()
        {
            return await _context.Personas
                .Include(p => p.Vehiculos)
                .ToListAsync();
        }

        public async Task<Persona?> ObtenerPorIdAsync(int id)
        {
            // .FindAsync(id) es excelente, pero no soporta .Include(), por lo que lo cambiamos
            return await _context.Personas
                .Include(p => p.Vehiculos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AgregarAsync(Persona persona)
        {
            await _context.Personas.AddAsync(persona);
            await _context.SaveChangesAsync();
        }

        public async Task<Persona?> ObtenerPorIdentificacionAsync(string identificacion)
        {
            return await _context.Personas
                .Include(p => p.Vehiculos)
                .FirstOrDefaultAsync(p => p.Identificacion == identificacion);
        }

        public async Task EditarPorIdentificacionAsync(string identificacion, Persona persona)
        {
            var encontrado = await ObtenerPorIdentificacionAsync(identificacion);

            if (encontrado == null)
                throw new Exception($"No existe persona con identificación {identificacion}");

            encontrado.Nombre = persona.Nombre;
            encontrado.Apellidos = persona.Apellidos;
            encontrado.Telefono = persona.Telefono;
            encontrado.Salario = persona.Salario;

            _context.Personas.Update(encontrado);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarPorIdentificacionAsync(string identificacion)
        {
            var persona = await ObtenerPorIdentificacionAsync(identificacion);

            if (persona == null)
                throw new Exception($"No existe persona con identificación {identificacion}");

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
        }
    }
}