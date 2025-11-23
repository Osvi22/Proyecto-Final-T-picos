using Proyecto.Model;
using System.Text;
using System.Text.Json;

namespace Proyecto.UI.Services
{
    public class ServicioApi
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ServicioApi(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiCliente");
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<Persona>> ListarPersonasAsync()
        {
            var response = await _httpClient.GetAsync("api/ServicioDePersonas/ObtengaLaLista");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Persona>>(stream, _jsonSerializerOptions) ?? new List<Persona>();
        }

        public async Task<Persona> ObtenerPersonaPorIdentificacionAsync(string identificacion)
        {
            var response = await _httpClient.GetAsync($"api/ServicioDePersonas/ConsultarPorIdentificacion/{identificacion}");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Persona>(stream, _jsonSerializerOptions) ?? new Persona();
        }

        public async Task CrearPersonaAsync(Persona persona)
        {

            var json = JsonSerializer.Serialize(persona);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/ServicioDePersonas/Agregue", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task EditarPersonaAsync(Persona persona)
        {

            var json = JsonSerializer.Serialize(persona);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/ServicioDePersonas/EditarPorIdentificacion/{persona.Identificacion}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Vehiculo>> ListarVehiculosAsync()
        {
            var response = await _httpClient.GetAsync("api/ServicioDeVehiculos/Listar");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Vehiculo>>(stream, _jsonSerializerOptions) ?? new List<Vehiculo>();
        }

        public async Task<List<Vehiculo>> ListarVehiculosPorIdentificacionAsync(string identificacion)
        {
            var response = await _httpClient.GetAsync($"api/ServicioDeVehiculos/ConsultarPorIdentificacion/{identificacion}");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Vehiculo>>(stream, _jsonSerializerOptions) ?? new List<Vehiculo>();
        }

        public async Task CrearVehiculoAsync(Vehiculo vehiculo)
        {
            var json = JsonSerializer.Serialize(vehiculo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/ServicioDeVehiculos/Agregar", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al crear vehículo ({(int)response.StatusCode}): {errorContent}");
            }
        }
        public async Task<Vehiculo> ObtenerVehiculoPorIdAsync(int id)
        {

            var response = await _httpClient.GetAsync($"api/ServicioDeVehiculos/ObtenerPorId/{id}");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Vehiculo>(stream, _jsonSerializerOptions) ?? new Vehiculo();
        }

        public async Task EditarVehiculoAsync(int vehiculoId, VehiculoEditarDto vehiculoDto)
        {
            var json = JsonSerializer.Serialize(vehiculoDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/ServicioDeVehiculos/Editar/{vehiculoId}", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al editar vehículo ({(int)response.StatusCode}): {errorContent}");
            }
        }

        public async Task EliminarPersonaAsync(string identificacion)
        {
            var response = await _httpClient.DeleteAsync($"api/ServicioDePersonas/EliminarPorIdentificacion/{identificacion}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al eliminar persona ({(int)response.StatusCode}): {errorContent}");
            }
        }

        public async Task EliminarVehiculoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ServicioDeVehiculos/Eliminar/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al eliminar vehículo ({(int)response.StatusCode}): {errorContent}");
            }
        }
    }
}