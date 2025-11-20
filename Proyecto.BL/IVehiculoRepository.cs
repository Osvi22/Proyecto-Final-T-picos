using Proyecto.Model;

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
