using Microsoft.AspNetCore.Mvc;
using Proyecto.Model;
using Proyecto.UI.Services;
using System.Diagnostics; // <-- Asegúrate de importar esto
using Proyecto.UI.Models; // <-- Y de importar tu ErrorViewModel

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
                // --- CORREGIDO ---
                // Ahora pasamos el modelo correcto a la vista de Error.
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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al crear la persona: {ex.Message}");
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
                // --- CORREGIDO ---
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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al editar la persona: {ex.Message}");
                }
            }
            return View(persona);
        }
    }
}