using AionClass.Frontend.Models;
using AionClass.Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AionClass.Frontend.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUserService _userService;

        public UsuariosController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _userService.ObterTodosAsync();
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser usuario)
        {
            if (ModelState.IsValid)
            {
                await _userService.CriarAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var usuario = await _userService.ObterPorIdAsync(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser usuario)
        {
            if (usuario.Id != null)
            {
                await _userService.AtualizarAsync(usuario.Id, usuario);
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var usuario = await _userService.ObterPorIdAsync(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _userService.DeletarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
