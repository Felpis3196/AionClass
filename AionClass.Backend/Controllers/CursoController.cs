using AionClass.Backend.Models;
using AionClass.Backend.Services.Implementations;
using AionClass.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AionClass.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class CursosController : Controller
    {
        private readonly ICursoService _cursosService;

        public CursosController(ICursoService cursoService)
        {
            _cursosService = cursoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matriculas = await _cursosService.ObterTodosAsync();
            return Ok(matriculas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var curso = await _cursosService.ObterPorIdAsync(id);
            if (curso == null)
                return NotFound();

            return Ok(curso);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Create([FromBody] Curso novoCurso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var criada = await _cursosService.CriarAsync(novoCurso);
            return CreatedAtAction(nameof(GetById), new { id = criada.Id }, criada);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Update(int id, [FromBody] Curso cursoAtualizado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var atualizada = await _cursosService.AtualizarAsync(id, cursoAtualizado);
            if (atualizada == null)
                return NotFound();

            return Ok(atualizada);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _cursosService.DeletarAsync(id);
            if (!sucesso)
                return NotFound();

            return NoContent();
        }
    }
}
