using AionClass.Frontend.Models.Auth;
using Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            HttpContext.Session.SetString("JwtToken", token);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            var errorMessage = BuildFullErrorMessage(ex);
            ModelState.AddModelError(string.Empty, errorMessage);
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
        if (model.Email == null)
            return View(model);

        if(model.Role == null)
        {
            model.Role = "Student";
            model.PerfilUsuario = "Student";
        }

        if (string.IsNullOrEmpty(model.Avatar))
        {
            model.Avatar = "https://meusite.com/imagens/avatar-padrao.png";
        }

        try
        {
            var token = await _authService.RegisterAsync(model);
            HttpContext.Session.SetString("JwtToken", token);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            var errorMessage = BuildFullErrorMessage(ex);
            ModelState.AddModelError(string.Empty, errorMessage);
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        // Remove token customizado da sessão
        HttpContext.Session.Remove("JwtToken");

        // Desloga o usuário da autenticação do ASP.NET Identity
        await HttpContext.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }


    private string BuildFullErrorMessage(Exception ex)
    {
        var message = ex.Message;

        if (ex.InnerException != null)
        {
            message += $" | Inner: {ex.InnerException.Message}";
        }

        // Use isso se quiser detalhes mais completos do erro (stack trace, etc.)
        // message += $"\n\nDetalhes:\n{ex}";

        return message;
    }
}
