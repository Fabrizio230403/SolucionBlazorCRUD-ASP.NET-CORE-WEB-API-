using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        private readonly SistemaConsultoriaContext _sistemaConsultoriaContext;

        public EmpleadoController(SistemaConsultoriaContext sistemaConsultoriaContext)
        {
            _sistemaConsultoriaContext = sistemaConsultoriaContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<UsuarioDTO>>();
            var listaUsuarioDTO = new List<UsuarioDTO>();

            try
            {
                foreach (var item in await _sistemaConsultoriaContext.Usuarios.Include(d => d.Rol).ToListAsync())
                {
                    listaUsuarioDTO.Add(new UsuarioDTO
                    {
                         UsuarioId = item.UsuarioId,
                         Nombre = item.Nombre,
                         Email = item.Email,
                         PasswordHash = item.PasswordHash,
                         RolId = item.RolId,
                         FechaCreacion = item.FechaCreacion,
                         Rol = new RolDTO
                         {
                             RolId = item.Rol!.RolId,
                             Nombre = item.Rol.Nombre,
                             Descripcion = item.Rol.Descripcion,
                         }

                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaUsuarioDTO;

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<UsuarioDTO>();
            var UsuarioDTO = new UsuarioDTO();

            try
            {

                var dbUsuario = await _sistemaConsultoriaContext.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == id);

                if(dbUsuario != null)
                {
                    UsuarioDTO.UsuarioId = dbUsuario.UsuarioId;
                    UsuarioDTO.Nombre = dbUsuario.Nombre;
                    UsuarioDTO.Email = dbUsuario.Email;
                    UsuarioDTO.PasswordHash = dbUsuario.PasswordHash;
                    UsuarioDTO.RolId = dbUsuario.RolId;
                    UsuarioDTO.FechaCreacion = dbUsuario.FechaCreacion;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = UsuarioDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }
                
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(UsuarioDTO usuario)
        {
            var responseApi = new ResponseAPI<int>();
             
            try
            {

                var dbUsuario = new Usuario
                {
                    Nombre = usuario.Nombre,
                    Email = usuario.Email,
                    PasswordHash = usuario.PasswordHash,
                    RolId = usuario.RolId,
                    FechaCreacion = usuario.FechaCreacion,
                };

                _sistemaConsultoriaContext.Usuarios.Add(dbUsuario);
                await _sistemaConsultoriaContext.SaveChangesAsync();

                if(dbUsuario.UsuarioId != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbUsuario.UsuarioId;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No guardado";
                }
                 
                
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(UsuarioDTO usuario, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbUsuario = await _sistemaConsultoriaContext.Usuarios.FirstOrDefaultAsync(e => e.UsuarioId == id);
               

                if (dbUsuario != null)
                {

                    dbUsuario.Nombre = usuario.Nombre;
                    dbUsuario.Email = usuario.Email;
                    dbUsuario.PasswordHash = usuario.PasswordHash;
                    dbUsuario.RolId = usuario.RolId;
                    dbUsuario.FechaCreacion = usuario.FechaCreacion;


                    _sistemaConsultoriaContext.Usuarios.Update(dbUsuario);
                    await _sistemaConsultoriaContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbUsuario.UsuarioId;               
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Usuario no encontrado";
                }


            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbUsuario = await _sistemaConsultoriaContext.Usuarios.FirstOrDefaultAsync(e => e.UsuarioId == id);


                if (dbUsuario != null)
                {              
                    _sistemaConsultoriaContext.Usuarios.Remove(dbUsuario);
                    await _sistemaConsultoriaContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                     
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Usuario no encontrado";
                }


            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


    }
}
