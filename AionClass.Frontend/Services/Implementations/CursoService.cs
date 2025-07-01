using AionClass.Frontend.Models;
using AionClass.Frontend.Services.Interfaces;

namespace AionClass.Frontend.Services.Implementations
{
    public class CursoService : ICursoService
    {
        private readonly HttpClient _httpClient;

        public CursoService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("API");
        }

        public async Task<IEnumerable<Curso>> ObterTodosAsync()
        {
            var response = await _httpClient.GetAsync("api/Cursos");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Curso>>();
        }

        public async Task<Curso> ObterPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Cursos/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Curso>();
        }

        public async Task<Curso> CriarAsync(Curso curso)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Cursos", curso);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Curso>();
        }

        public async Task<Curso> AtualizarAsync(int id, Curso curso)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Cursos/{id}", curso);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Curso>();
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Cursos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
