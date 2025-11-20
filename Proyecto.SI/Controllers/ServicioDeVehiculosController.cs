using Microsoft.AspNetCore.Mvc;
using Proyecto.BL;
using Proyecto.Model;

namespace Proyecto.SI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioDeVehiculosController : ControllerBase
    {
        private readonly IAdministracionDeVehiculo _admin;

        public ServicioDeVehiculosController(IAdministracionDeVehiculo admin)
        {
            _admin = admin;
        }


        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> Listar()
        {
            var vehiculos = await _admin.ObtengaLaListaAsync();
            return Ok(vehiculos);
        }

        [HttpGet("ConsultarPorIdentificacion/{identificacion}")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> ConsultarPorIdentificacion(string identificacion)
        {
            var vehiculos = await _admin.ObtengaVehiculosPorIdentificacionAsync(identificacion);

            if (!vehiculos.Any())
                return NotFound($"No se encontraron vehículos para la identificación {identificacion}.");

            return Ok(vehiculos);
        }

        [HttpGet("ObtenerPorId/{id:int}")]
        public async Task<ActionResult<Vehiculo>> ObtenerPorId(int id)
        {
            var vehiculo = await _admin.ObtengaElVehiculoAsync(id);
            if (vehiculo == null)
            {
                return NotFound($"No se encontró vehículo con ID {id}");
            }
            return Ok(vehiculo);
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult> Agregar([FromBody] Vehiculo vehiculo)
        {
            if (vehiculo == null)
                return BadRequest("Debe enviar los datos del vehículo.");

            await _admin.AgregueAsync(vehiculo);

            return Ok("Vehículo agregado correctamente.");
        }


        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] VehiculoEditarDto vehiculoDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vehiculoExistente = await _admin.ObtengaElVehiculoAsync(id);
            if (vehiculoExistente == null) return NotFound($"No existe vehículo con id {id}.");

            vehiculoExistente.Placa = vehiculoDto.Placa;
            vehiculoExistente.Marca = vehiculoDto.Marca;
            vehiculoExistente.Modelo = vehiculoDto.Modelo;
            vehiculoExistente.PersonaId = vehiculoDto.PersonaId;

            await _admin.EditeAsync(vehiculoExistente);

            return Ok("Vehículo editado correctamente.");
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var vehiculo = await _admin.ObtengaElVehiculoAsync(id);

            if (vehiculo == null)
                return NotFound($"No existe el vehículo con id {id}.");

            await _admin.ElimineAsync(id);

            return Ok("Vehículo eliminado correctamente.");
        }

    }
}