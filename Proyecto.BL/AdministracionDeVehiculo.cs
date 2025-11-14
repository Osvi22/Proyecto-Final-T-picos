using Proyecto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BL
{
    public class AdministracionDeVehiculo : IAdministracionDeVehiculo
    {
        private readonly IVehiculoRepository _vehiculoRepository;

        public AdministracionDeVehiculo(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }
        public async Task AgregueAsync(Vehiculo vehiculo)
        {
            await _vehiculoRepository.AgregarAsync(vehiculo);
        }

        public async Task<IEnumerable<Vehiculo>> ObtengaLaListaAsync()
        {
            return await _vehiculoRepository.ObtenerTodosAsync();
        }

        public async Task<Vehiculo?> ObtengaElVehiculoAsync(int id)
        {
            return await _vehiculoRepository.ObtenerPorIdAsync(id);
        }

        public async Task<IEnumerable<Vehiculo>> ObtengaVehiculosPorIdentificacionAsync(string identificacion)
        {
            return await _vehiculoRepository.ObtenerPorIdentificacionAsync(identificacion);
        }

        public async Task EditeAsync(Vehiculo vehiculo)
        {
            await _vehiculoRepository.ActualizarAsync(vehiculo);
        }

        public async Task ElimineAsync(int id)
        {
            await _vehiculoRepository.EliminarAsync(id);
        }
    }
}