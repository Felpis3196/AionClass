using AionClass.Frontend.Models.Auth;
using Frontend.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AionClass.Frontend.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("API");
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new UnauthorizedAccessException($"Erro no login: {error}");
            }

            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();

            return content?.Message ?? "Login realizado com sucesso.";
        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Erro no registro: {error}");
            }

            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();

            return content?.Message ?? "Usuário registrado com sucesso.";
        }

        public async Task LogoutAsync()
        {
            var response = await _httpClient.PostAsync("api/Auth/logout", null);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Erro no logout: {error}");
            }
        }

        private class ApiResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}
