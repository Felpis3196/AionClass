using AionClass.Frontend.Models;
using AionClass.Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AionClass.Frontend.Controllers
{
    public class CursosController : Controller
    {
        private readonly ICursoService _cursoService;

        public CursosController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        public async Task<IActionResult> Index()
        {
            var cursos = await _cursoService.ObterTodosAsync();
            return View(cursos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var curso = await _cursoService.ObterPorIdAsync(id);
            return View(curso);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Curso curso)
        {
            if (!ModelState.IsValid)
                return View(curso);

            await _cursoService.CriarAsync(curso);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var curso = await _cursoService.ObterPorIdAsync(id);
            return View(curso);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Curso curso)
        {
            if (!ModelState.IsValid)
                return View(curso);

            await _cursoService.AtualizarAsync(id, curso);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _cursoService.ObterPorIdAsync(id);
            return View(curso);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cursoService.DeletarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
