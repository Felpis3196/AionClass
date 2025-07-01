using AionClass.Frontend.Models.Auth;
using Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var token = await _authService.LoginAsync(model);
            HttpContext.Session.SetString("JwtToken", token); // Armazena o token na sessão
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var token = await _authService.RegisterAsync(model);
            HttpContext.Session.SetString("JwtToken", token); // Armazena o token já no registro
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("JwtToken"); // Remove o token da sessão
        return RedirectToAction("Login");
    }
}
