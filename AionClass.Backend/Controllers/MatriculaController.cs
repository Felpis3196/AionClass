using AionClass.Backend.DTOs.Auth;
using AionClass.Backend.Models;
using AionClass.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AionClass.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class MatriculasController : ControllerBase
    {
        private readonly IMatriculaService _matriculaService;

        public MatriculasController(IMatriculaService matriculaService)
        {
            _matriculaService = matriculaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matriculas = await _matriculaService.ObterTodasAsync();
            return Ok(matriculas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var matricula = await _matriculaService.ObterPorIdAsync(id);
            if (matricula == null)
                return NotFound();

            return Ok(matricula);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuarioId(string usuarioId)
        {
            var matriculas = await _matriculaService.ObterPorUsuarioIdAsync(usuarioId);
            return Ok(matriculas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Matricula novaMatricula)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var criada = await _matriculaService.CriarAsync(novaMatricula);
            return CreatedAtAction(nameof(GetById), new { id = criada.Id }, criada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Matricula matriculaAtualizada)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var atualizada = await _matriculaService.AtualizarAsync(id, matriculaAtualizada);
            if (atualizada == null)
                return NotFound();

            return Ok(atualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _matriculaService.DeletarAsync(id);
            if (!sucesso)
                return NotFound();

            return NoContent();
        }

        [HttpGet("verificar/{usuarioId}/{curso}")]
        public async Task<IActionResult> VerificarMatricula(string usuarioId, string curso)
        {
            var existe = await _matriculaService.UsuarioPossuiMatriculaAsync(usuarioId, curso);
            return Ok(new { possuiMatricula = existe });
        }
    }
}