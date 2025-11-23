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
                    TempData["SuccessMessage"] = "Vehículo creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError("", $"Error del Backend: {ex.Message}");
                    TempData["ErrorMessage"] = "No se pudo crear el vehículo.";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
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

                    TempData["SuccessMessage"] = "Vehículo actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError("", $"Error del Backend: {ex.Message}");
                    TempData["ErrorMessage"] = "No se pudo actualizar el vehículo.";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
                }
            }
            return View(vehiculo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _servicioApi.EliminarVehiculoAsync(id);
                TempData["SuccessMessage"] = "Vehículo eliminado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar el vehículo: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}