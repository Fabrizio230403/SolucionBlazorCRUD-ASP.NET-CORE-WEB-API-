using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
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
                foreach (var item in await _dbContext.Tareas.Include(p => p.Proyecto).Include(u => u.UsuarioAsignado).ToListAsync())
                {
                    listaTareaDTO.Add(new TareaDTO
                    {
                        TareaId = item.TareaId,
                        ProyectoId = item.ProyectoId,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        FechaInicio = item.FechaInicio,
                        FechaFin = item.FechaFin,
                        Estado = item.Estado,
                        UsuarioAsignadoId = item.UsuarioAsignadoId,
                        Proyecto = new ProyectoDTO
                        {
                            ProyectoId = item.Proyecto!.ProyectoId,
                            Nombre = item.Proyecto.Nombre,
                            Descripcion = item.Proyecto.Descripcion,
                            FechaInicio = item.Proyecto.FechaInicio,
                            FechaFin = item.Proyecto.FechaFin,
                            Prioridad = item.Proyecto.Prioridad,
                            Estado = item.Proyecto.Estado,
                            GerenteId = item.Proyecto.GerenteId,
                            PorcentajeCompleto = item.Proyecto.PorcentajeCompleto,
                        },
                        Usuario = new UsuarioDTO
                        {
                            UsuarioId = item.UsuarioAsignado!.UsuarioId,
                            Nombre = item.UsuarioAsignado.Nombre,
                            Email = item.UsuarioAsignado.Email,
                            PasswordHash = item.UsuarioAsignado.PasswordHash,
                            RolId = item.UsuarioAsignado.RolId,
                            FechaCreacion = item.UsuarioAsignado.FechaCreacion,
                        }
                        

                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaTareaDTO;

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
            var responseApi = new ResponseAPI<TareaDTO>();
            var TareaDTO = new TareaDTO();

            try
            {
                var dbTarea = await _dbContext.Tareas.FirstOrDefaultAsync(x => x.TareaId == id);

                if (dbTarea != null)
                {
                    TareaDTO.TareaId = dbTarea.TareaId;
                    TareaDTO.ProyectoId = dbTarea.ProyectoId;
                    TareaDTO.Nombre = dbTarea.Nombre;
                    TareaDTO.Descripcion = dbTarea.Descripcion;
                    TareaDTO.FechaInicio = dbTarea.FechaInicio;
                    TareaDTO.FechaFin = dbTarea.FechaFin;
                    TareaDTO.Estado = dbTarea.Estado;
                    TareaDTO.UsuarioAsignadoId = dbTarea.UsuarioAsignadoId;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = TareaDTO;
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
                    ProyectoId = tarea.ProyectoId,
                    Nombre = tarea.Nombre,
                    Descripcion = tarea.Descripcion,
                    FechaInicio = tarea.FechaInicio,
                    FechaFin = tarea.FechaFin,
                    Estado = tarea.Estado,
                    UsuarioAsignadoId = tarea.UsuarioAsignadoId,
                };

                _dbContext.Tareas.Add(dbTarea);
                await _dbContext.SaveChangesAsync();

                if (dbTarea.TareaId != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTarea.TareaId;
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
        public async Task<IActionResult> Editar(TareaDTO tarea, int id)
        {
            var responseApi = new ResponseAPI<int>();


            try
            {

                var dbTarea = await _dbContext.Tareas.FirstOrDefaultAsync(e => e.TareaId == id);


                if (dbTarea != null)
                {

                    dbTarea.ProyectoId = tarea.ProyectoId;
                    dbTarea.Nombre = tarea.Nombre;
                    dbTarea.Descripcion = tarea.Descripcion;
                    dbTarea.FechaInicio = tarea.FechaInicio;
                    dbTarea.FechaFin = tarea.FechaFin;        
                    dbTarea.Estado = tarea.Estado;
                    dbTarea.UsuarioAsignadoId = tarea.UsuarioAsignadoId;

                    _dbContext.Tareas.Update(dbTarea);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTarea.TareaId;

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

                var dbTarea = await _dbContext.Tareas.FirstOrDefaultAsync(e => e.TareaId == id);


                if (dbTarea != null)
                {

                    _dbContext.Tareas.Remove(dbTarea);
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


        [HttpGet]
        [Route("Lista/{usuarioId}")]
        public async Task<IActionResult> ListaPorUsuario(int usuarioId)
        {
            var responseApi = new ResponseAPI<List<TareaDTO>>();
            var listaTareaDTO = new List<TareaDTO>();

            try
            {
                // Filtrar tareas por UsuarioAsignadoId
                foreach (var item in await _dbContext.Tareas
                    .Where(t => t.UsuarioAsignadoId == usuarioId) // Filtrar tareas por el usuario
                    .Include(p => p.Proyecto)
                    .Include(u => u.UsuarioAsignado)
                    .ToListAsync())
                {
                    listaTareaDTO.Add(new TareaDTO
                    {
                        TareaId = item.TareaId,
                        ProyectoId = item.ProyectoId,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        FechaInicio = item.FechaInicio,
                        FechaFin = item.FechaFin,
                        Estado = item.Estado,
                        UsuarioAsignadoId = item.UsuarioAsignadoId,
                        Proyecto = new ProyectoDTO
                        {
                            ProyectoId = item.Proyecto!.ProyectoId,
                            Nombre = item.Proyecto.Nombre,
                            Descripcion = item.Proyecto.Descripcion,
                            FechaInicio = item.Proyecto.FechaInicio,
                            FechaFin = item.Proyecto.FechaFin,
                            Prioridad = item.Proyecto.Prioridad,
                            Estado = item.Proyecto.Estado,
                            GerenteId = item.Proyecto.GerenteId,
                            PorcentajeCompleto = item.Proyecto.PorcentajeCompleto,
                        },
                        Usuario = new UsuarioDTO
                        {
                            UsuarioId = item.UsuarioAsignado!.UsuarioId,
                            Nombre = item.UsuarioAsignado.Nombre,
                            Email = item.UsuarioAsignado.Email,
                            PasswordHash = item.UsuarioAsignado.PasswordHash,
                            RolId = item.UsuarioAsignado.RolId,
                            FechaCreacion = item.UsuarioAsignado.FechaCreacion,
                        }
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaTareaDTO;
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
