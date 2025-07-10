using AionClass.Frontend.Models;
using AionClass.Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AionClass.Frontend.Controllers
{
    public class MatriculaController : Controller
    {
        private readonly IMatriculaService _matriculaService;
        private readonly ICursoService _cursoService;

        public MatriculaController(IMatriculaService matriculaService, ICursoService cursoService)
        {
            _matriculaService = matriculaService;
            _cursoService = cursoService;
        }

        // GET: Matricula
        public async Task<IActionResult> Index()
        {
            var matriculas = await _matriculaService.ObterTodasAsync();
            return View(matriculas);
        }

        // GET: Matricula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _matriculaService.ObterPorIdAsync(id.Value);
            if (matricula == null) return NotFound();

            return View(matricula);
        }

        // GET: Matricula/Create
        public async Task<IActionResult> Create()
        {
            var cursos = await _cursoService.ObterTodosAsync();
            ViewData["CursoId"] = new SelectList(cursos, "Id", "Title");
            return View();
        }

        // POST: Matricula/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CursoId,DataMatricula")] Matricula matricula)
        {
            // Pega o ID do usuário autenticado
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            // Preenche o campo automaticamente
            matricula.ApplicationUserId = userId;

            if (ModelState.IsValid)
            {
                await _matriculaService.CriarAsync(matricula);
                return RedirectToAction(nameof(Index));
            }

            var cursos = await _cursoService.ObterTodosAsync();
            ViewData["CursoId"] = new SelectList(cursos, "Id", "Title", matricula.CursoId);
            return View(matricula);
        }


        // GET: Matricula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _matriculaService.ObterPorIdAsync(id.Value);
            if (matricula == null) return NotFound();

            var cursos = await _cursoService.ObterTodosAsync();
            ViewData["CursoId"] = new SelectList(cursos, "Id", "Title", matricula.CursoId);

            return View(matricula);
        }

        // POST: Matricula/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CursoId,DataMatricula,ApplicationUserId")] Matricula matricula)
        {
            if (id != matricula.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var atualizado = await _matriculaService.AtualizarAsync(id, matricula);
                if (atualizado == null) return NotFound();

                return RedirectToAction(nameof(Index));
            }

            var cursos = await _cursoService.ObterTodosAsync();
            ViewData["CursoId"] = new SelectList(cursos, "Id", "Title", matricula.CursoId);

            return View(matricula);
        }

        // GET: Matricula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var matricula = await _matriculaService.ObterPorIdAsync(id.Value);
            if (matricula == null) return NotFound();

            return View(matricula);
        }

        // POST: Matricula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultado = await _matriculaService.DeletarAsync(id);
            if (!resultado) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
