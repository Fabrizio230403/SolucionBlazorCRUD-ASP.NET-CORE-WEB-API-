using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Shared;
using BlazorCrud.Server.Models;
using Microsoft.EntityFrameworkCore;
using BlazorCrud.Server.Custom;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly SistemaConsultoriaContext _context;
        private readonly Utilidades _utilidades;

        // Inyección del contexto de base de datos y utilidades para encriptación y generación de JWT
        public UsuarioController(SistemaConsultoriaContext context, Utilidades utilidades)
        {
            _context = context;
            _utilidades = utilidades;
        }



        /*[HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            // Busca en la base de datos el usuario que coincide con el correo y la clave (PasswordHash)
            var usuario = await _context.Usuarios
                .Where(u => u.Email == login.Correo && u.PasswordHash == login.Clave)
                .Select(u => new UsuarioDTO
                {
                    UsuarioId = u.UsuarioId,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    RolId = u.RolId,
                    FechaCreacion = u.FechaCreacion,
                    Rol = new RolDTO
                    {
                        RolId = u.Rol.RolId,
                        Nombre = u.Rol.Nombre
                    }
                })
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                // Retorna un código 401 si las credenciales son incorrectas
                return StatusCode(StatusCodes.Status401Unauthorized, "Usuario o clave incorrectos");
            }

            // Crear el DTO de sesión usando los datos del usuario encontrado
            SesionDTO sesionDTO = new SesionDTO
            {
                Nombre = usuario.Nombre,
                Correo = usuario.Email,
                Rol = usuario.Rol?.Nombre ?? "Sin Rol"
            };

            // Retorna la sesión con el código de éxito
            return StatusCode(StatusCodes.Status200OK, sesionDTO);
        }*/


        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroDTO registro)
        {
            // Verifica si el correo ya está en uso
            if (await _context.Usuarios.AnyAsync(u => u.Email == registro.Correo))
            {
                return BadRequest("El correo ya está registrado.");
            }

            // Hashea la contraseña antes de guardarla
            var passwordHash = _utilidades.HashPassword(registro.Clave);

            int rolConsultorId = 3; // O reemplázalo con el valor adecuado si tu rol "Consultor" tiene otro ID en la base de datos.


            var usuario = new Usuario
            {
                Nombre = registro.Nombre,
                Email = registro.Correo,
                PasswordHash = passwordHash,
                RolId = rolConsultorId,  // Asignamos el rol "Consultor"
                FechaCreacion = DateTime.Now
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado con éxito");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var passwordHash = _utilidades.HashPassword(login.Clave);

            var usuario = await _context.Usuarios
                .Where(u => u.Email == login.Correo && u.PasswordHash == passwordHash)
                .Select(u => new UsuarioDTO
                {
                    UsuarioId = u.UsuarioId,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash, // Asegúrate de que PasswordHash esté aquí
                    RolId = u.RolId,
                    FechaCreacion = u.FechaCreacion,
                    Rol = new RolDTO
                    {
                        RolId = u.Rol!.RolId,
                        Nombre = u.Rol.Nombre,
                        Descripcion = u.Rol.Descripcion
                    }
                })
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Usuario o clave incorrectos");
            }

            var token = _utilidades.GenerarToken(usuario);
            var response = new
            {
                Token = token,
                Usuario = usuario
            };

            return Ok(response);
        }

        [HttpPost("RecuperarContraseña")]
        public async Task<IActionResult> RecuperarContraseña([FromBody] RecuperarContraseñaDTO model)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Correo);
            if (usuario == null)
            {
                return BadRequest("El correo no está registrado.");
            }

            // Genera un token de recuperación y guarda en la base de datos 

            var token = Guid.NewGuid().ToString();   
            usuario.TokenRecuperacion = token;  
            usuario.FechaExpiracionToken = DateTime.UtcNow.AddHours(1);   
            await _context.SaveChangesAsync();

            var enlace = $"http://localhost:5208/restablecer-contraseña?token={token}";

            var emailService = new EmailService();
            try
            {
                await emailService.EnviarCorreoRecuperacionAsync(model.Correo, enlace);
                return Ok("Correo enviado exitosamente.");
            }
            catch (Exception ex)
            {
                 
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                return StatusCode(500, "Error al enviar el correo.");
            }
        }

        [HttpPost("RestablecerContraseña")]
        public async Task<IActionResult> RestablecerContraseña([FromBody] RestablecerContraseñaDTO model)
        {
            // Verifica que el token sea válido
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.TokenRecuperacion == model.Token && u.FechaExpiracionToken > DateTime.UtcNow);

            if (usuario == null)
            {
                return BadRequest("El token es inválido o ha expirado.");
            }

             
            usuario.PasswordHash = _utilidades.HashPassword(model.NuevaClave);  
            usuario.TokenRecuperacion = null;   
            usuario.FechaExpiracionToken = null;  
            await _context.SaveChangesAsync();

            return Ok("Contraseña restablecida exitosamente.");
        }
    }
}
