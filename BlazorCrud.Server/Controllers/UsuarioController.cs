using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Shared;
using BlazorCrud.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly SistemaConsultoriaContext _context;

        // Inyección del contexto de base de datos
        public UsuarioController(SistemaConsultoriaContext context)
        {
            _context = context;
        }

        /*[HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            SesionDTO sesionDTO = new SesionDTO();
            if (login.Correo == "admin@gmail.com" && login.Clave == "admin")
            {
                sesionDTO.Nombre = "admin";
                sesionDTO.Correo = login.Correo;
                sesionDTO.Rol =  "Administrador";

            }
            else
            {
                sesionDTO.Nombre = "empleado";
                sesionDTO.Correo = login.Correo;
                sesionDTO.Rol = "Empleado";
            }
            return StatusCode(StatusCodes.Status200OK, sesionDTO);
        }*/

        [HttpPost]
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
        }
    }
}
