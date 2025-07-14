using AionClass.Frontend.Models;
using AionClass.Frontend.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AionClass.Frontend.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = clientFactory.CreateClient("API");

            var token = httpContextAccessor.HttpContext?.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IEnumerable<ApplicationUser>> ObterTodosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/User");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<ApplicationUser>>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Não foi possível conectar à API de usuários.", ex);
            }
            catch (NotSupportedException)
            {
                throw new Exception("O conteúdo da resposta da API não está em um formato compatível.");
            }
            catch (System.Text.Json.JsonException)
            {
                throw new Exception("Erro ao processar os dados de usuários recebidos da API.");
            }
        }

        public async Task<ApplicationUser> ObterPorIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/User/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ApplicationUser>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro de conexão ao buscar usuário na API.", ex);
            }
            catch (NotSupportedException)
            {
                throw new Exception("Formato de resposta da API inválido ao buscar usuário.");
            }
            catch (System.Text.Json.JsonException)
            {
                throw new Exception("Erro ao desserializar os dados do usuário.");
            }
        }

        public async Task<ApplicationUser> CriarAsync(ApplicationUser novoUsuario)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/User", novoUsuario);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ApplicationUser>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao conectar e criar usuário na API.", ex);
            }
            catch (NotSupportedException)
            {
                throw new Exception("Formato de resposta inválido ao criar usuário.");
            }
            catch (System.Text.Json.JsonException)
            {
                throw new Exception("Erro ao processar a resposta de criação do usuário.");
            }
        }

        public async Task<ApplicationUser> AtualizarAsync(string id, ApplicationUser usuarioAtualizado)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/User/{id}", usuarioAtualizado);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ApplicationUser>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao conectar e atualizar o usuário na API.", ex);
            }
            catch (NotSupportedException)
            {
                throw new Exception("Resposta da API inválida ao atualizar usuário.");
            }
            catch (System.Text.Json.JsonException)
            {
                throw new Exception("Erro ao processar a resposta da atualização do usuário.");
            }
        }

        public async Task<bool> DeletarAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/User/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao conectar e deletar o usuário na API.", ex);
            }
        }

        public async Task<UserPerfilViewModel> ObterPerfilAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/user/perfil");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao buscar perfil. Status: {response.StatusCode}, Detalhes: {errorContent}");
                }

                var perfil = await response.Content.ReadFromJsonAsync<UserPerfilViewModel>();
                return perfil!;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao buscar dados do perfil do usuário autenticado.", ex);
            }
        }
    }
}
