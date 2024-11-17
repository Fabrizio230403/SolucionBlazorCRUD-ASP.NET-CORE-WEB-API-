using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {

        private readonly SistemaConsultoriaContext _sistemaConsultoriaContext;

        public RolController(SistemaConsultoriaContext sistemaConsultoriaContext)
        {
            _sistemaConsultoriaContext = sistemaConsultoriaContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<RolDTO>>();
            var listaRolDTO = new List<RolDTO>();

            try
            {
                foreach(var item in await _sistemaConsultoriaContext.Roles.ToListAsync())
                {
                    listaRolDTO.Add(new RolDTO
                    {
                        RolId = item.RolId,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaRolDTO;

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
