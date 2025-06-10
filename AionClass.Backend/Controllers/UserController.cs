using AionClass.Backend.Models;
using AionClass.Backend.Services.Implementations;
using AionClass.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AionClass.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
    }
}
