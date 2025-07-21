using AionClass.Frontend.Models.Auth;
using Frontend.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
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
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/login", request);
                var responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Response Body:");
                Console.WriteLine(responseBody);

                if (!response.IsSuccessStatusCode)
                {
                    throw new UnauthorizedAccessException($"Erro no login: {responseBody}");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var content = JsonSerializer.Deserialize<AuthResponse>(responseBody, options);

                if (content == null)
                {
                    throw new Exception("Resposta nula na desserialização");
                }

                if (!content.Success)
                {
                    throw new UnauthorizedAccessException(content.Message ?? "Falha no login");
                }

                return content.Token;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro de conexão com o servidor durante o login.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Erro ao processar a resposta de login.", ex);
            }
        }
        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/register", request);
                var responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Resposta recebida do servidor: " + responseBody);

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Erro no registro: {responseBody}");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var content = JsonSerializer.Deserialize<AuthResponse>(responseBody, options);

                if (content == null || !content.Success)
                {
                    throw new ApplicationException($"Erro no registro: {content?.Message ?? "Resposta inválida"}");
                }

                return content.Token;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro de conexão com o servidor durante o registro.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Erro ao processar a resposta de registro.", ex);
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("api/Auth/logout", null);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Erro no logout: {responseBody}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro de conexão com o servidor durante o logout.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Erro ao processar a resposta de logout.", ex);
            }
        }

        private class ApiResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}
