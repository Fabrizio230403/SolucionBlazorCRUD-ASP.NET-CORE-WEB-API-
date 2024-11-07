using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly SistemaConsultoriaContext _dbContext;

        public TareaController(SistemaConsultoriaContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<TareaDTO>>();
            var listaTareaDTO = new List<TareaDTO>();

            try
            {
                foreach (var item in await _dbContext.Tarea.ToListAsync())
                {
                    listaTareaDTO.Add(new TareaDTO
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        ProyectoId = item.ProyectoId,
                        Descripcion = item.Descripcion,
                        FechaInicio = item.FechaInicio,
                        FechaFin = item.FechaFin,
                        Prioridad = item.Prioridad,
                        Estado = item.Estado,
                        FechaCreacion = item.FechaCreacion,
                        FechaActualizacion = item.FechaActualizacion,
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaTareaDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<TareaDTO>();
            var tareaDTO = new TareaDTO();

            try
            {
                var dbTarea = await _dbContext.Tarea.FirstOrDefaultAsync(x => x.Id == id);

                if (dbTarea != null)
                {
                    tareaDTO.Id = dbTarea.Id;
                    tareaDTO.Nombre = dbTarea.Nombre;
                    tareaDTO.Descripcion = dbTarea.Descripcion;
                    tareaDTO.FechaInicio = dbTarea.FechaInicio;
                    tareaDTO.FechaFin = dbTarea.FechaFin;
                    tareaDTO.Prioridad = dbTarea.Prioridad;
                    tareaDTO.Estado = dbTarea.Estado;
                    tareaDTO.FechaCreacion = dbTarea.FechaCreacion;
                    tareaDTO.FechaActualizacion = dbTarea.FechaActualizacion;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = tareaDTO;
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
        public async Task<IActionResult> Guardar(TareaDTO tarea)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTarea = new Tarea
                {
                    Nombre = tarea.Nombre,
                    Descripcion = tarea.Descripcion,
                    FechaInicio = tarea.FechaInicio,
                    FechaFin = tarea.FechaFin,
                    Prioridad = tarea.Prioridad,
                    Estado = tarea.Estado,
                    FechaCreacion = tarea.FechaCreacion,
                    FechaActualizacion = tarea.FechaActualizacion,
                    ProyectoId = tarea.ProyectoId
                };

                _dbContext.Tarea.Add(dbTarea);
                await _dbContext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = dbTarea.Id;
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
        public async Task<IActionResult> Editar(TareaDTO tarea, int id)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbTarea = await _dbContext.Tarea.FirstOrDefaultAsync(e => e.Id == id);

                if (dbTarea != null)
                {
                    dbTarea.Nombre = tarea.Nombre;
                    dbTarea.Descripcion = tarea.Descripcion;
                    dbTarea.FechaInicio = tarea.FechaInicio;
                    dbTarea.FechaFin = tarea.FechaFin;
                    dbTarea.Prioridad = tarea.Prioridad;
                    dbTarea.Estado = tarea.Estado;
                    dbTarea.FechaActualizacion = DateTime.Now;

                    _dbContext.Tarea.Update(dbTarea);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTarea.Id;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Tarea no encontrada";
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
                var dbTarea = await _dbContext.Tarea.FirstOrDefaultAsync(e => e.Id == id);

                if (dbTarea != null)
                {
                    _dbContext.Tarea.Remove(dbTarea);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Tarea no encontrada";
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
