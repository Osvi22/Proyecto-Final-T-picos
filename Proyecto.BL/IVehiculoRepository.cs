using Proyecto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BL
{
    public interface IVehiculoRepository
    {
        Task AgregarAsync(Vehiculo vehiculo);
        Task<IEnumerable<Vehiculo>> ObtenerTodosAsync();
        Task<Vehiculo?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Vehiculo>> ObtenerPorIdentificacionAsync(string identificacion);
        Task ActualizarAsync(Vehiculo vehiculo);
        Task EliminarAsync(int id);

    }


}
