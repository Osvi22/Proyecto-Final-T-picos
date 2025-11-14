using Microsoft.AspNetCore.Mvc;
using Proyecto.BL;
using Proyecto.Model;

namespace Proyecto.SI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioDePersonasController : ControllerBase
    {
        private readonly IAdministradorDePersonas _admin;

        public ServicioDePersonasController(IAdministradorDePersonas admin)
        {
            _admin = admin;
        }

        [HttpGet("ObtengaLaLista")]
        public async Task<ActionResult<IEnumerable<Persona>>> ObtengaLaLista()
        {
            var lista = await _admin.ObtengaLaListaAsync();
            return Ok(lista);
        }



        [HttpGet("ObtengaLaPersona/{id:int}")]
        public async Task<ActionResult<Persona>> ObtengaLaPersona(int id)
        {
            var persona = await _admin.ObtengaLaPersonaAsync(id);
            if (persona == null)
                return NotFound($"No se encontró persona con ID {id}");
            return Ok(persona);
        }



        [HttpPost("Agregue")]
        public async Task<IActionResult> Agregue([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _admin.AgregueAsync(persona);
            return Ok("Persona agregada correctamente.");
        }


        [HttpGet("ConsultarPorIdentificacion/{identificacion}")]
        public async Task<ActionResult<Persona>> ConsultarPorIdentificacion(string identificacion)
        {
            var persona = await _admin.ObtengaPorIdentificacionAsync(identificacion);
            if (persona == null)
                return NotFound($"No se encontró una persona con identificación {identificacion}");
            return Ok(persona);
        }


        [HttpPut("EditarPorIdentificacion/{identificacion}")]
        public async Task<IActionResult> EditarPorIdentificacion(string identificacion, [FromBody] Persona persona)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            try
            {
                await _admin.EditePorIdentificacionAsync(identificacion, persona);
                return Ok($"Persona con identificación {identificacion} actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("EliminarPorIdentificacion/{identificacion}")]
        public async Task<IActionResult> EliminarPorIdentificacion(string identificacion)
        {
            try
            {
                await _admin.EliminePorIdentificacionAsync(identificacion);
                return Ok($"Persona con identificación {identificacion} eliminada correctamente");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}