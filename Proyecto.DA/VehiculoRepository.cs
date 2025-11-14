using Proyecto.BL;
using Proyecto.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DA

{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly DBContexto _context;

        public VehiculoRepository(DBContexto context)
        {
            _context = context;
        }
        public async Task AgregarAsync(Vehiculo vehiculo)
        {
            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vehiculo>> ObtenerTodosAsync()
        {
            return await _context.Vehiculos
                .Include(v => v.Persona)
                .ToListAsync();
        }

        public async Task<Vehiculo?> ObtenerPorIdAsync(int id)
        {
            return await _context.Vehiculos
                .Include(v => v.Persona)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Vehiculo>> ObtenerPorIdentificacionAsync(string identificacion)
        {
            return await _context.Vehiculos
                .Include(v => v.Persona)
                .Where(v => v.Persona.Identificacion == identificacion)
                .ToListAsync();
        }

        public async Task ActualizarAsync(Vehiculo vehiculo)
        {
            _context.Vehiculos.Update(vehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var vehiculo = await ObtenerPorIdAsync(id);

            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
                await _context.SaveChangesAsync();
            }
        }
    }
}