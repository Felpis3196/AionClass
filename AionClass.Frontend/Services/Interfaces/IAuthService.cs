using AionClass.Frontend.Models.Auth;

namespace Frontend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequest request);
        Task<string> RegisterAsync(RegisterRequest request);
        Task LogoutAsync();
    }
}
