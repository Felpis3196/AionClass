using AionClass.Frontend.Models;
using AionClass.Frontend.Services.Interfaces;
using System.Net.Http.Json;

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
            try
            {
                var response = await _httpClient.GetAsync("api/Cursos");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<Curso>>();
            }
            catch (HttpRequestException ex)
            {
                // API desligada, problema de conexão
                throw new Exception("Não foi possível conectar à API. Tente novamente mais tarde.", ex);
            }
            catch (NotSupportedException)
            {
                throw new Exception("O conteúdo retornado não está no formato esperado.");
            }
            catch (System.Text.Json.JsonException)
            {
                throw new Exception("Erro ao processar os dados retornados da API.");
            }
        }

        public async Task<Curso> ObterPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Cursos/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Curso>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro de conexão ao buscar o curso. Verifique a API.", ex);
            }
            catch (NotSupportedException)
            {
                throw new Exception("Resposta da API com formato inválido.");
            }
            catch (System.Text.Json.JsonException)
            {
                throw new Exception("Erro ao desserializar os dados do curso.");
            }
        }

        public async Task<Curso> CriarAsync(Curso curso)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Cursos", curso);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Curso>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao conectar e criar o curso na API.", ex);
            }
            catch (NotSupportedException)
            {
                throw new Exception("Resposta da API inválida ao criar curso.");
            }
            catch (System.Text.Json.JsonException)
            {
                throw new Exception("Erro ao processar a resposta da criação do curso.");
            }
        }

        public async Task<Curso> AtualizarAsync(int id, Curso curso)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Cursos/{id}", curso);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Curso>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao conectar e atualizar o curso na API.", ex);
            }
            catch (NotSupportedException)
            {
                throw new Exception("Resposta da API inválida ao atualizar curso.");
            }
            catch (System.Text.Json.JsonException)
            {
                throw new Exception("Erro ao processar a resposta da atualização do curso.");
            }
        }

        public async Task<bool> DeletarAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Cursos/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao conectar e deletar o curso na API.", ex);
            }
        }
    }
}
