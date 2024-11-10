using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlazorCrud.Server.Models;
using BlazorCrud.Shared;

namespace BlazorCrud.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PermisoController : Controller
    {
        private readonly SistemaConsultoriaContext _sistemaContext;

        public PermisoController(SistemaConsultoriaContext sistemaConsultoriaContext)
        {
            _sistemaContext = sistemaConsultoriaContext;
        }



        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {

            var ResponseAPI = new ResponseAPI<List<PermisoDTO>>();
            var listaPermisoDTO = new List<PermisoDTO>();

            try
            {
                foreach( var item in await _sistemaContext.Permiso.ToListAsync() )
                {
                    listaPermisoDTO.Add(new PermisoDTO()
                    {
                        PermisoID = item.PermisoID,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                    });
                }

                ResponseAPI.EsCorrecto = true;
                ResponseAPI.Valor = listaPermisoDTO;

            }
            catch (Exception ex)
            {
                ResponseAPI.EsCorrecto = false;
                ResponseAPI.Mensaje = ex.Message;
            }

            return Ok(ResponseAPI);

        }


    }
}
