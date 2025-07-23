using AionClass.Backend.DTOs.Auth;
using AionClass.Backend.Models;
using AionClass.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace AionClass.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userService.ObterTodosAsync();
            return Ok(user);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.ObterPorIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ApplicationUser novoUsuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioCriado = await _userService.CriarAsync(novoUsuario);
            return CreatedAtAction(nameof(GetById), new { id = usuarioCriado.Id }, usuarioCriado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var sucesso = await _userService.DeletarAsync(id);
            if (!sucesso)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ApplicationUser userAtualizado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var atualizada = await _userService.AtualizarAsync(id, userAtualizado);
            if (atualizada == null)
                return NotFound();

            return Ok(atualizada);
        }

        [HttpGet("perfil")]
        public async Task<ActionResult<UserPerfilViewModel>> ObterPerfil()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"UserId do token: {userId}");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Token inválido ou expirado.");
            }

            var usuario = await _userManager.FindByIdAsync(userId);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var viewModel = new UserPerfilViewModel
            {
                Id = usuario.Id,
                PrimeiroNome = usuario.PrimeiroNome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                PhoneNumber = usuario.PhoneNumber,
                Avatar = usuario.Avatar,
                Role = usuario.Role,
            };

            return Ok(viewModel);
        }
    }
}
