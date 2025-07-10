using AionClass.Frontend.Models;
using AionClass.Frontend.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace AionClass.Frontend.Services.Implementations
{
    public class MatriculaService : IMatriculaService
    {
        private readonly HttpClient _httpClient;

        public MatriculaService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("API");
        }

        public async Task<IEnumerable<Matricula>> ObterTodasAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Matricula>>("api/Matriculas");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter a lista de matrículas.", ex);
            }
        }

        public async Task<Matricula> ObterPorIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Matriculas/{id}");
                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<Matricula>();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar matrícula por ID.", ex);
            }
        }

        public async Task<IEnumerable<Matricula>> ObterPorUsuarioIdAsync(string usuarioId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Matricula>>($"api/Matriculas/usuario/{usuarioId}");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar matrículas do usuário.", ex);
            }
        }

        public async Task<Matricula> CriarAsync(Matricula matricula)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Matriculas", matricula);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Matricula>();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar matrícula.", ex);
            }
        }

        public async Task<Matricula> AtualizarAsync(int id, Matricula matriculaAtualizada)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Matriculas/{id}", matriculaAtualizada);
                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<Matricula>();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar matrícula.", ex);
            }
        }

        public async Task<bool> DeletarAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Matriculas/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar matrícula.", ex);
            }
        }

        public async Task<bool> UsuarioPossuiMatriculaAsync(string usuarioId, string curso)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Matriculas/verificar/{usuarioId}/{Uri.EscapeDataString(curso)}");
                if (!response.IsSuccessStatusCode)
                    return false;

                var result = await response.Content.ReadFromJsonAsync<VerificarMatriculaResponse>();
                return result?.PossuiMatricula ?? false;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar matrícula existente para o usuário.", ex);
            }
        }

        private class VerificarMatriculaResponse
        {
            public bool PossuiMatricula { get; set; }
        }
    }
}
