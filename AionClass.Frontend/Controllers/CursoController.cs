using Microsoft.AspNetCore.Mvc;
using AionClass.Frontend.Models;
using AionClass.Frontend.Services.Interfaces;

namespace AionClass.Frontend.Controllers
{
    public class CursoController : Controller
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        // GET: Curso
        public async Task<IActionResult> Index()
        {
            var cursos = await _cursoService.ObterTodosAsync();
            return View(cursos);
        }

        // GET: Curso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var curso = await _cursoService.ObterPorIdAsync(id.Value);
            if (curso == null) return NotFound();

            return View(curso);
        }

        // GET: Curso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Curso/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Curso curso)
        {
            if (ModelState.IsValid)
            {
                await _cursoService.CriarAsync(curso);
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        // GET: Curso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var curso = await _cursoService.ObterPorIdAsync(id.Value);
            if (curso == null) return NotFound();

            return View(curso);
        }

        // POST: Curso/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Curso curso)
        {
            if (id != curso.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _cursoService.AtualizarAsync(id, curso);
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        // GET: Curso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var curso = await _cursoService.ObterPorIdAsync(id.Value);
            if (curso == null) return NotFound();

            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cursoService.DeletarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
 