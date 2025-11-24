using Microsoft.AspNetCore.Mvc;
using Proyecto.Model;
using Proyecto.UI.Services;
using System.Diagnostics; 
using Proyecto.UI.Models; 

namespace Proyecto.UI.Controllers
{
    public class PersonasController : Controller
    {
        private readonly ServicioApi _servicioApi;

        public PersonasController(ServicioApi servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var personas = await _servicioApi.ListarPersonasAsync();
                return View(personas);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = ex.Message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persona persona)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _servicioApi.CrearPersonaAsync(persona);
                    TempData["SuccessMessage"] = "Persona creada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al crear la persona: {ex.Message}");
                    TempData["ErrorMessage"] = "No se pudo crear la persona.";
                }
            }
            return View(persona);
        }

        public async Task<IActionResult> Edit(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
            {
                return BadRequest("La identificación no puede ser nula.");
            }

            try
            {
                var persona = await _servicioApi.ObtenerPersonaPorIdentificacionAsync(identificacion);
                if (persona == null)
                {
                    return NotFound();
                }
                return View(persona);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = ex.Message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string identificacion, Persona persona)
        {
            if (identificacion != persona.Identificacion)
            {
                return BadRequest("La identificación de la ruta no coincide con la del formulario.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _servicioApi.EditarPersonaAsync(persona);
                    TempData["SuccessMessage"] = "Persona editada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error al editar: {ex.Message}";
                    ModelState.AddModelError("", $"Error al editar la persona: {ex.Message}");
                }
            }
            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string identificacion)
        {
            try
            {
                await _servicioApi.EliminarPersonaAsync(identificacion);
                TempData["SuccessMessage"] = "Persona eliminada correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar la persona: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}