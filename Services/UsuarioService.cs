using Microsoft.EntityFrameworkCore;
using PruebaCursos.Models;

namespace PruebaCursos.Services
{
    public class UsuarioService
    {
        private readonly PlataformaEvaluacionCursosDev4Context _context;

        public UsuarioService(PlataformaEvaluacionCursosDev4Context context)
        {
            _context = context;
        }

        // Método para listar usuarios
        public async Task<List<Usuario>> ListarUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // Método para registrar un usuario
        public async Task<bool> RegistrarUsuarioAsync(Usuario nuevoUsuario)
        {
            // Validate input
            if (nuevoUsuario == null)
            {
                Console.WriteLine("Usuario es nulo.");
                return false;
            }

            // Validate email format
            if (!IsValidEmail(nuevoUsuario.Email))
            {
                Console.WriteLine("Formato de correo electrónico inválido.");
                return false;
            }

            // Validate password strength
            if (!IsValidPassword(nuevoUsuario.Contraseña))
            {
                Console.WriteLine("La contraseña no cumple con los requisitos de seguridad.");
                return false;
            }

            try
            {
                // Check if email already exists
                var existe = await _context.Usuarios.AnyAsync(u => u.Email == nuevoUsuario.Email);
                if (existe)
                {
                    Console.WriteLine("El correo ya está registrado.");
                    return false;
                }

                // Hash password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(nuevoUsuario.Contraseña);

                // Create new user
                var usuarioParaRegistrar = new Usuario
                {
                    Nombre = nuevoUsuario.Nombre,
                    Email = nuevoUsuario.Email,
                    Contraseña = hashedPassword,
                    FechaRegistro = DateTime.Now
                };

                // Add and save user
                _context.Usuarios.Add(usuarioParaRegistrar);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the full exception details
                Console.WriteLine($"Error al registrar el usuario: {ex.Message}");
                Console.WriteLine($"Detalles del error: {ex}");
                return false;
            }
        }

        // Email validation method
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Password strength validation method
        private bool IsValidPassword(string password)
        {
            // Example rules: 
            // - At least 8 characters
            // - Contains at least one uppercase letter
            // - Contains at least one lowercase letter
            // - Contains at least one number
            return !string.IsNullOrWhiteSpace(password) &&
                   password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit);
        }



    }
}
