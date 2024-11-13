using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RolPermisoController : ControllerBase
    {

        private readonly SistemaConsultoriaContext _sistemaContext;

        public RolPermisoController(SistemaConsultoriaContext sistemaContext)
        {
            _sistemaContext = sistemaContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<RolPermisoDTO>>();
            var listaRolPermisoDTO = new List<RolPermisoDTO>();

            try
            {
                foreach (var item in await _sistemaContext.Roles_Permisos.ToListAsync())
                {
                    listaRolPermisoDTO.Add(new RolPermisoDTO
                    {
                        rolId = item.RolId,
                        permisoId = item.PermisoId,
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaRolPermisoDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);

        }


        [HttpGet]
        [Route("ListaPermisos/{usuarioId}")]
        public async Task<IActionResult> ListaPermisos(int usuarioId)
        {
            var responseApi = new ResponseAPI<List<string>>();

            try
            {
                var usuario = await _sistemaContext.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == usuarioId);

                if (usuario != null)
                {
                    var permisoNombres = await _sistemaContext.Roles_Permisos
                        .Where(rp => rp.RolId == usuario.RolId)
                        .Join(_sistemaContext.Permiso,
                              rp => rp.PermisoId,
                              p => p.PermisoID,
                              (rp, p) => p.Nombre)
                        .ToListAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = permisoNombres;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Usuario no encontrado.";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Error general: " + ex;
            }

            return Ok(responseApi);
        }


    }
}
