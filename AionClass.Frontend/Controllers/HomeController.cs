using AionClass.Frontend.Models;
using AionClass.Frontend.Models.ViewModels;
using AionClass.Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AionClass.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IMatriculaService _matriculaService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IMatriculaService matriculaService)
        {
            _logger = logger;
            _userService = userService;
            _matriculaService = matriculaService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            ViewBag.IsAdmin = false;

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var perfil = await _userService.ObterPerfilAsync();
                    ViewBag.IsAdmin = perfil?.Role == "Admin";
                }
                catch{}
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Cursos()
        {
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var token = HttpContext.Session.GetString("JwtToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var perfil = await _userService.ObterPerfilAsync();

            var matriculas = await _matriculaService.ObterPorUsuarioIdAsync(perfil.Id);

            perfil.Matricula = matriculas.Select(m => new MatriculaViewModel
            {
                CursoId = m.CursoId,
                CursoTitulo = m.Curso?.Title,
                ThumbnailUrl = m.Curso?.ThumbnailUrl,
                DataMatricula = m.DataMatricula
            }).ToList();

            return View(perfil);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
