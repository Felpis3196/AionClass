using Microsoft.AspNetCore.Mvc;
using AionClass.Frontend.Models;
using AionClass.Frontend.Services.Interfaces;

namespace AionClass.Frontend.Controllers
{
    public class CursoController : Controller
    {
        private readonly ICursoService _cursoService;
        private readonly IMatriculaService _matriculaService;
        private readonly IUserService _userService;

        public CursoController(
        ICursoService cursoService,
        IMatriculaService matriculaService,
        IUserService userService)
        {
            _cursoService = cursoService;
            _matriculaService = matriculaService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JwtToken");

            ViewBag.IsAdmin = false;
            ViewBag.IsInstructor = false;

            var cursos = await _cursoService.ObterTodosAsync();

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var perfil = await _userService.ObterPerfilAsync();
                    ViewBag.IsAdmin = perfil?.Role == "Admin";
                    ViewBag.IsInstructor = perfil?.Role == "Instructor";
                    ViewBag.IsStudent = perfil?.Role == "Student";
                    ViewBag.NotAuthed = perfil?.Role == null;
                }
                catch {}
            }
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

        public async Task<IActionResult> Matricular(int id)
        {
            try
            {
                // Obtem o perfil do usuário logado
                var perfil = await _userService.ObterPerfilAsync();
                if (perfil == null || string.IsNullOrEmpty(perfil.Id))
                {
                    TempData["Mensagem"] = "Usuário não autenticado.";
                    return RedirectToAction(nameof(Index));
                }

                // Verifica se já está matriculado
                bool jaMatriculado = await _matriculaService.UsuarioPossuiMatriculaAsync(perfil.Id, id.ToString());

                if (jaMatriculado)
                {
                    TempData["Mensagem"] = "Você já está matriculado neste curso.";
                    return RedirectToAction(nameof(Index));
                }

                // Cria nova matrícula
                var novaMatricula = new Matricula
                {
                    ApplicationUserId = perfil.Id,
                    CursoId = id,
                    DataMatricula = DateTime.UtcNow
                };

                await _matriculaService.CriarAsync(novaMatricula);
                TempData["Mensagem"] = "Matrícula realizada com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = $"Erro ao realizar matrícula: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
 