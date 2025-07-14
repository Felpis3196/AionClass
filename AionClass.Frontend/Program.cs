using AionClass.Frontend.Data;
using AionClass.Frontend.Models;
using AionClass.Frontend.Services.Implementations;
using AionClass.Frontend.Services.Interfaces;
using Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Ativar uso de sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Habilitar HttpContextAccessor para acessar Session nos serviços
builder.Services.AddHttpContextAccessor();

// 🔐 Delegating Handler para adicionar o JWT automaticamente nas requisições
builder.Services.AddTransient<JwtTokenHandler>();

// Base URL da API externa (definida em appsettings.json -> ApiSettings:BaseUrl)
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];

// Configurar HttpClient nomeado com JwtTokenHandler
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
})
.AddHttpMessageHandler<JwtTokenHandler>(); // ← aplica o handler que insere o JWT

// Registrar serviços personalizados
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Pipeline de requisição
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession(); // deve vir após UseRouting

// Rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
