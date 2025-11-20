using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Model;
using Proyecto.UI.Models;
using Proyecto.UI.Services;

namespace Proyecto.UI.Controllers
{
    public class PropietariosController : Controller
    {
        private readonly ServicioApi _servicioApi;

        public PropietariosController(ServicioApi servicioApi)
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

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var vehiculo = await _servicioApi.ObtenerVehiculoPorIdAsync(id);
                var personas = await _servicioApi.ListarPersonasAsync();

                if (vehiculo == null)
                {
                    return NotFound();
                }

                var viewModel = new PropietarioEditViewModel
                {
                    Vehiculo = vehiculo,
                    ListaDePersonas = new SelectList(personas, "Id", "Nombre", vehiculo.PersonaId),
                    PersonaIdSeleccionada = vehiculo.PersonaId
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml", new Models.ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PropietarioEditViewModel viewModel)
        {
            if (id != viewModel.Vehiculo.Id)
            {
                return BadRequest();
            }

            try
            {
                var vehiculoAActualizar = await _servicioApi.ObtenerVehiculoPorIdAsync(id);
                if (vehiculoAActualizar == null)
                {
                    return NotFound();
                }

                vehiculoAActualizar.PersonaId = viewModel.PersonaIdSeleccionada;

                var vehiculoDto = new VehiculoEditarDto
                {
                    Placa = vehiculoAActualizar.Placa,
                    Marca = vehiculoAActualizar.Marca,
                    Modelo = vehiculoAActualizar.Modelo,
                    PersonaId = vehiculoAActualizar.PersonaId
                };

                await _servicioApi.EditarVehiculoAsync(id, vehiculoDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var personas = await _servicioApi.ListarPersonasAsync();
                viewModel.ListaDePersonas = new SelectList(personas, "Id", "Nombre", viewModel.PersonaIdSeleccionada);
                ModelState.AddModelError("", $"Error al editar el propietario: {ex.Message}");
                return View(viewModel);
            }
        }
    }
}