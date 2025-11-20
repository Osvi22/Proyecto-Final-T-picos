using Microsoft.AspNetCore.Mvc;
using Proyecto.Model;
using Proyecto.UI.Services;

namespace Proyecto.UI.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly ServicioApi _servicioApi;

        public VehiculosController(ServicioApi servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var vehiculos = await _servicioApi.ListarVehiculosAsync();
                return View(vehiculos);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml", new Models.ErrorViewModel { Message = ex.Message });
            }
        }

        public IActionResult Create()
        {
            return View(new Vehiculo { PersonaId = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _servicioApi.CrearVehiculoAsync(vehiculo);
                    return RedirectToAction(nameof(Index));
                }
                catch (HttpRequestException ex) // Maneja el error detallado del API
                {
                    ModelState.AddModelError("", $"Error del Backend al crear el vehículo: {ex.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al crear el vehículo: {ex.Message}");
                }
            }
            return View(vehiculo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var vehiculo = await _servicioApi.ObtenerVehiculoPorIdAsync(id);
                if (vehiculo == null)
                {
                    return NotFound();
                }
                return View(vehiculo);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml", new Models.ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vehiculoExistente = await _servicioApi.ObtenerVehiculoPorIdAsync(id);
                    if (vehiculoExistente == null) return NotFound();

                    vehiculoExistente.Placa = vehiculo.Placa;
                    vehiculoExistente.Marca = vehiculo.Marca;
                    vehiculoExistente.Modelo = vehiculo.Modelo;
                    vehiculoExistente.PersonaId = vehiculo.PersonaId; 

                    var vehiculoDto = new VehiculoEditarDto
                    {
                        Placa = vehiculoExistente.Placa,
                        Marca = vehiculoExistente.Marca,
                        Modelo = vehiculoExistente.Modelo,
                        PersonaId = vehiculoExistente.PersonaId
                    };

                    await _servicioApi.EditarVehiculoAsync(id, vehiculoDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (HttpRequestException ex) 
                {
                    ModelState.AddModelError("", $"Error del Backend al editar el vehículo: {ex.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al editar el vehículo: {ex.Message}");
                }
            }
            return View(vehiculo);
        }
    }
}