using Microsoft.AspNetCore.Mvc;
using Proyecto.Model;
using Proyecto.UI.Services;

namespace Proyecto.UI.Controllers
{
    public class PersonasController : Controller
    {
        private readonly ServicioApi _servicioApi;

        // 1. Inyectamos el ServicioApi en el constructor
        public PersonasController(ServicioApi servicioApi)
        {
            _servicioApi = servicioApi;
        }

        // GET: /Personas
        // Muestra la lista de todas las personas
        public async Task<IActionResult> Index()
        {
            try
            {
                var personas = await _servicioApi.ListarPersonasAsync();
                return View(personas); // Pasa la lista a la vista
            }
            catch (Exception ex)
            {
                // Manejar el error (ej. mostrar una vista de error)
                return View("Error", new { message = ex.Message });
            }
        }

        // GET: /Personas/Create
        // Muestra el formulario para crear una nueva persona
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Personas/Create
        // Recibe los datos del formulario y los envía a la API
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persona persona)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _servicioApi.CrearPersonaAsync(persona);
                    return RedirectToAction(nameof(Index)); // Regresa a la lista
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al crear la persona: {ex.Message}");
                }
            }
            return View(persona); // Muestra el formulario de nuevo con errores
        }

        // GET: /Personas/Edit/5
        // Muestra el formulario para editar una persona
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var persona = await _servicioApi.ObtenerPersonaPorIdAsync(id);
                return View(persona);
            }
            catch (Exception ex)
            {
                return View("Error", new { message = ex.Message });
            }
        }

        // POST: /Personas/Edit/5
        // Recibe los datos del formulario y los envía a la API
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Persona persona)
        {
            if (id != persona.Id)
            {
                return BadRequest();
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