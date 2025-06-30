using AionClass.Frontend.Services.Interfaces;
using AionClass.Frontend.Models.Auth;
using Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AionClass.Frontend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var message = await _authService.LoginAsync(request);
                return Ok(new { success = true, message });
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized(new { success = false, message = e.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var message = await _authService.RegisterAsync(request);
                return Ok(new { success = true, message });
            }
            catch (ApplicationException e)
            {
                return BadRequest(new { success = false, message = e.Message });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _authService.LogoutAsync();
            return Ok(new { success = true, message = "Logout realizado com sucesso." });
        }
    }
}
