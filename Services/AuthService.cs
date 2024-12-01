using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using PruebaCursos.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public class AuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PlataformaEvaluacionCursosDev4Context _context;

    public AuthService(IHttpContextAccessor httpContextAccessor, PlataformaEvaluacionCursosDev4Context context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    // Método para autenticar un usuario
    public async Task<bool> Login(string email, string password)
    {
        Console.WriteLine($"Email recibido: {email}");
        Console.WriteLine($"Password recibido: {password}");
        // Validar email
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            throw new ArgumentException("El email no es válido.", nameof(email));
        }

        // Validar contraseña
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("La contraseña no puede estar vacía.", nameof(password));
        }

        // Verificar el contexto HTTP
        if (_httpContextAccessor.HttpContext == null)
        {
            throw new InvalidOperationException("El contexto HTTP no está disponible.");
        }

        // Buscar usuario en la base de datos
        var user = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null || !VerifyPassword(password, user.Contraseña))
        {
            throw new UnauthorizedAccessException("Email o contraseña incorrectos.");
        }

        // Crear los claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Nombre),
            new Claim("UsuarioId", user.UsuarioId.ToString())
        };

       
        // Crear la identidad
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Configurar las propiedades de autenticación
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, // Mantener la sesión activa tras cerrar el navegador
            ExpiresUtc = DateTime.UtcNow.AddHours(8) // Establecer la expiración de la cookie
        };

        // Iniciar sesión
        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return true;
    }

    private bool VerifyPassword(string inputPassword, string storedPassword)
    {
        // Aquí debes implementar la lógica para comparar contraseñas.
        // Si las contraseñas están hasheadas, usa el método correspondiente para validar.
        return inputPassword == storedPassword;
    }

    public async Task Logout()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }





}
