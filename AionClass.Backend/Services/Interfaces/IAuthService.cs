using AionClass.Backend.DTOs.Auth;

namespace AionClass.Backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequest request);
        Task<string> RegisterAsync(RegisterRequest request);

    }
}
