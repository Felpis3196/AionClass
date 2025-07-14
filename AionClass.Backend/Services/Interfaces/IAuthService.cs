using AionClass.Backend.DTOs.Auth;

namespace AionClass.Backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterAsync(RegisterRequest request);

    }
}
