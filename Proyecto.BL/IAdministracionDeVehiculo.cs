using Proyecto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BL
{
    public interface IAdministracionDeVehiculo
    {
        Task AgregueAsync(Vehiculo vehiculo);
        Task<IEnumerable<Vehiculo>> ObtengaLaListaAsync();
        Task<Vehiculo?> ObtengaElVehiculoAsync(int id);
        Task<IEnumerable<Vehiculo>> ObtengaVehiculosPorIdentificacionAsync(string identificacion);
        Task EditeAsync(Vehiculo vehiculo);
        Task ElimineAsync(int id);
       
    }
}