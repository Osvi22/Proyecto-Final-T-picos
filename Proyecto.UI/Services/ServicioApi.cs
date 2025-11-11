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
            var response = await _httpClient.GetAsync("api/Personas");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Persona>>(stream, _jsonSerializerOptions) ?? new List<Persona>();
        }

        public async Task<Persona> ObtenerPersonaPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Personas/{id}");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Persona>(stream, _jsonSerializerOptions) ?? new Persona();
        }

        public async Task CrearPersonaAsync(Persona persona)
        {
            var json = JsonSerializer.Serialize(persona);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Personas", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task EditarPersonaAsync(Persona persona)
        {
            var json = JsonSerializer.Serialize(persona);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
           
            var response = await _httpClient.PutAsync($"api/Personas/{persona.Id}", content);
            response.EnsureSuccessStatusCode();
        }
    }
}