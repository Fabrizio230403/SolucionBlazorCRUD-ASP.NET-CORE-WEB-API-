using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly SistemaConsultoriaContext _dbContext;

        public ProyectoController(SistemaConsultoriaContext dbContext)
        {
            _dbContext = dbContext;
        }



        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<ProyectoDTO>>();
            var listaProyectoDTO = new List<ProyectoDTO>();

            try
            {
                foreach (var item in await _dbContext.Proyectos.ToListAsync())
                {
                    listaProyectoDTO.Add(new ProyectoDTO
                    {
                        ProyectoID = item.ProyectoID,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        FechaInicio = item.FechaInicio,
                        FechaFin = item.FechaFin,
                        Prioridad = item.Prioridad,
                        Estado = item.Estado,
                        GerenteID = item.GerenteID,
                        PorcentajeCompleto = item.PorcentajeCompleto
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaProyectoDTO;
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
            var responseApi = new ResponseAPI<ProyectoDTO>();
            var ProyectoDTO = new ProyectoDTO();

            try
            {
                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(x => x.ProyectoID == id);

                if (dbProyecto != null)
                {
                    ProyectoDTO.ProyectoID = dbProyecto.ProyectoID;
                    ProyectoDTO.Nombre = dbProyecto.Nombre;
                    ProyectoDTO.Descripcion = dbProyecto.Descripcion;
                    ProyectoDTO.FechaInicio = dbProyecto.FechaInicio;
                    ProyectoDTO.FechaFin = dbProyecto.FechaFin;
                    ProyectoDTO.Prioridad = dbProyecto.Prioridad;
                    ProyectoDTO.Estado = dbProyecto.Estado;
                    ProyectoDTO.GerenteID = dbProyecto.GerenteID;
                    ProyectoDTO.PorcentajeCompleto = dbProyecto.PorcentajeCompleto;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ProyectoDTO;
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
        public async Task<IActionResult> Guardar(ProyectoDTO proyecto)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProyecto = new Proyecto
                {
                    Nombre = proyecto.Nombre,
                    Descripcion = proyecto.Descripcion,
                    FechaInicio = proyecto.FechaInicio,
                    FechaFin = proyecto.FechaFin,
                    Prioridad = proyecto.Prioridad,
                    Estado = proyecto.Estado,
                    GerenteID = proyecto.GerenteID,
                    PorcentajeCompleto = proyecto.PorcentajeCompleto
                };

                _dbContext.Proyectos.Add(dbProyecto);
                await _dbContext.SaveChangesAsync();

                if (dbProyecto.ProyectoID != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProyecto.ProyectoID;
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
        public async Task<IActionResult> Editar(ProyectoDTO proyecto, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(e => e.ProyectoID == id);

                if (dbProyecto != null)
                {
                    dbProyecto.Nombre = proyecto.Nombre;
                    dbProyecto.Descripcion = proyecto.Descripcion;
                    dbProyecto.FechaInicio = proyecto.FechaInicio;
                    dbProyecto.FechaFin = proyecto.FechaFin;
                    dbProyecto.Prioridad = proyecto.Prioridad;
                    dbProyecto.Estado = proyecto.Estado;
                    dbProyecto.GerenteID = proyecto.GerenteID;
                    dbProyecto.PorcentajeCompleto = proyecto.PorcentajeCompleto;

                    _dbContext.Proyectos.Update(dbProyecto);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProyecto.ProyectoID;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Proyecto no encontrado";
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
            var responseApi = new ResponseAPI<bool>();

            try
            {
                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(e => e.ProyectoID == id);

                if (dbProyecto != null)
                {
                    _dbContext.Proyectos.Remove(dbProyecto);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Proyecto no encontrado";
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