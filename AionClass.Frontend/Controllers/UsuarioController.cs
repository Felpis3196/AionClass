using AionClass.Frontend.Models;
using AionClass.Frontend.Models.DTO;
using AionClass.Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AionClass.Frontend.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUserService _userService;

        public UsuarioController(IUserService userService)
        {
            _userService = userService;
        }

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
            if (usuario.Email != null)
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
        public async Task<IActionResult> EditPerfil(UserPerfilViewModel usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Id))
            {
                var user = await _userService.ObterPorIdAsync(usuario.Id);
                if (user == null) return NotFound();

                // Atualiza apenas campos permitidos
                user.PrimeiroNome = usuario.PrimeiroNome;
                user.Sobrenome = usuario.Sobrenome;
                user.Email = usuario.Email;
                user.PhoneNumber = usuario.PhoneNumber;
                user.Avatar = usuario.Avatar;

                await _userService.AtualizarAsync(user.Id, user);
                return RedirectToAction("Profile", "Home");
            }

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
