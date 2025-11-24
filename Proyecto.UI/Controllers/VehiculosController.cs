using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Model;
using Proyecto.UI.Models;
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

        public async Task<IActionResult> Create()
        {
            var personas = await _servicioApi.ListarPersonasAsync();
            ViewBag.ListaDePersonas = new SelectList(
                personas.Select(p => new { Id = p.Id, NombreCompleto = $"{p.Nombre} {p.Apellidos} ({p.Identificacion})" }),
                "Id", "NombreCompleto");
            return View();
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

            var personas = await _servicioApi.ListarPersonasAsync();
            ViewBag.ListaDePersonas = new SelectList(
                personas.Select(p => new { Id = p.Id, NombreCompleto = $"{p.Nombre} {p.Apellidos} ({p.Identificacion})" }),
                "Id", "NombreCompleto", vehiculo.PersonaId);
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

                var personas = await _servicioApi.ListarPersonasAsync();

                ViewBag.ListaDePersonas = new SelectList(
                    personas.Select(p => new {
                        Id = p.Id,
                        NombreCompleto = $"{p.Nombre} {p.Apellidos} ({p.Identificacion})"
                    }),
                    "Id",
                    "NombreCompleto",
                    vehiculo.PersonaId 
                );

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

                    var vehiculoDto = new VehiculoEditarDto
                    {
                        Placa = vehiculo.Placa,
                        Marca = vehiculo.Marca,
                        Modelo = vehiculo.Modelo,
                        PersonaId = vehiculo.PersonaId
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

            var personas = await _servicioApi.ListarPersonasAsync();
            ViewBag.ListaDePersonas = new SelectList(
                personas.Select(p => new {
                    Id = p.Id,
                    NombreCompleto = $"{p.Nombre} {p.Apellidos} ({p.Identificacion})"
                }),
                "Id",
                "NombreCompleto",
                vehiculo.PersonaId
            );

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