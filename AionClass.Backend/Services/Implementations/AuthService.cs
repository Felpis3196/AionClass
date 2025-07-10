using AionClass.Backend.DTOs.Auth;
using AionClass.Backend.Models;
using AionClass.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace AionClass.Backend.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userService = userService;
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                var errorResponse = new AuthResponse { Success = false, Message = "Credenciais inválidas" };
                return JsonSerializer.Serialize(errorResponse);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                var errorResponse = new AuthResponse { Success = false, Message = "Credenciais inválidas" };
                return JsonSerializer.Serialize(errorResponse);
            }

            var token = GenerateJwtToken(user);

            var successResponse = new AuthResponse { Success = true, Token = token, Message = "Login realizado com sucesso." };
            return JsonSerializer.Serialize(successResponse);
        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
            {
                var errorResponse = new AuthResponse { Success = false, Message = "Usuário já registrado com esse email." };
                return JsonSerializer.Serialize(errorResponse);
            }

            bool isFirstUser = !_userManager.Users.Any();

            string role = isFirstUser ? "Admin" : "Student";

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Email,
                Email = request.Email,
                Avatar = request.Avatar,
                PrimeiroNome = request.PrimeiroNome,
                Sobrenome = request.Sobrenome,
                Role = role,
                PerfilUsuario = role,
                EstaAtivo = true,
                DataCriacao = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                var errorResponse = new AuthResponse { Success = false, Message = errors };
                return JsonSerializer.Serialize(errorResponse);
            }

            await _userManager.AddToRoleAsync(user, role);

            return await LoginAsync(new LoginRequest
            {
                Email = request.Email,
                Password = request.Password
            });
        }
    }
}
