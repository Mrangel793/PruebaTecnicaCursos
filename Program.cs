using System.Text;
using Microsoft.EntityFrameworkCore;
using PruebaCursos.Components;
using PruebaCursos.Models;
using PruebaCursos.Services;
using static PruebaCursos.Components.Pages.Authentication.Login;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/login"; // Ruta para el inicio de sesi�n
        options.LogoutPath = "/logout"; // Ruta para cerrar sesi�n
        options.AccessDeniedPath = "/access-denied"; // Ruta para acceso denegado
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Aseg�rate de usar HTTPS
    });

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddDbContext<PlataformaEvaluacionCursosDev4Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Registro de servicios
builder.Services.AddScoped<CursoService>();
builder.Services.AddScoped<EvaluacionService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();




var app = builder.Build();




if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
