using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Shared;
using BlazorCrud.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly SistemaConsultoriaContext _context;

        public UsuarioController(SistemaConsultoriaContext context)
        {
            _context = context;
        }

        [HttpPost("Registro")]
        public async Task<IActionResult> Registro([FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null || string.IsNullOrEmpty(usuarioDTO.Email) || string.IsNullOrEmpty(usuarioDTO.PasswordHash) || usuarioDTO.RolId == 0)
            {
                return BadRequest("Los datos del usuario no son válidos.");
            }

            var existingUser = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == usuarioDTO.Email);
            if (existingUser != null)
            {
                return Conflict("El correo electrónico ya está en uso.");
            }

            var usuario = new Usuario
            {
                Nombre = usuarioDTO.Nombre,
                Email = usuarioDTO.Email,
                PasswordHash = HashPassword(usuarioDTO.PasswordHash),
                RolId = usuarioDTO.RolId,
                FechaCreacion = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuario registrado exitosamente" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var passwordHash = HashPassword(login.Clave);

            var usuario = await _context.Usuarios
                .Where(u => u.Email == login.Correo && u.PasswordHash == passwordHash)
                .Select(u => new UsuarioDTO
                {
                    UsuarioId = u.UsuarioId,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    RolId = u.RolId,
                    FechaCreacion = u.FechaCreacion,
                    Rol = new RolDTO
                    {
                        RolId = u.Rol!.RolId,
                        Nombre = u.Rol.Nombre
                    }
                })
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Usuario o clave incorrectos");
            }

            SesionDTO sesionDTO = new SesionDTO
            {
                Nombre = usuario.Nombre,
                Correo = usuario.Email,
                Rol = usuario.Rol?.Nombre ?? "Sin Rol"
            };

            return StatusCode(StatusCodes.Status200OK, sesionDTO);
        }


        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        private bool IsPasswordEncrypted(string passwordHash)
        {
            return passwordHash.Length == 44;
        }


    }
}
