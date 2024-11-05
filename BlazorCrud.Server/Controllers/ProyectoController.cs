using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                foreach(var item in await _dbContext.Proyectos.ToListAsync())
                {
                    listaProyectoDTO.Add(new ProyectoDTO
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        FechaInicio = item.FechaInicio,
                        FechaFin = item.FechaFin,
                        Prioridad = item.Prioridad,
                        Estado = item.Estado,
                        PresupuestoEstimado = item.PresupuestoEstimado,
                        RecursosAsignados = item.RecursosAsignados,
                        FechaCreacion = item.FechaCreacion,
                        FechaActualizacion = item.FechaActualizacion,
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaProyectoDTO;
            } catch (Exception ex)
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
                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(x => x.Id == id);

                if(dbProyecto != null)
                {
                    ProyectoDTO.Id = dbProyecto.Id;
                    ProyectoDTO.Nombre = dbProyecto.Nombre;
                    ProyectoDTO.Descripcion = dbProyecto.Descripcion;
                    ProyectoDTO.FechaInicio = dbProyecto.FechaInicio;
                    ProyectoDTO.FechaFin = dbProyecto.FechaFin;
                    ProyectoDTO.Prioridad = dbProyecto.Prioridad;
                    ProyectoDTO.Estado = dbProyecto.Estado;
                    ProyectoDTO.PresupuestoEstimado = dbProyecto.PresupuestoEstimado;
                    ProyectoDTO.RecursosAsignados = dbProyecto.RecursosAsignados;
                    ProyectoDTO.FechaCreacion = dbProyecto.FechaCreacion;
                    ProyectoDTO.FechaActualizacion = dbProyecto.FechaActualizacion;


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
                    PresupuestoEstimado = proyecto.PresupuestoEstimado,
                    RecursosAsignados = proyecto.RecursosAsignados,
                    FechaCreacion = proyecto.FechaCreacion,
                    FechaActualizacion = proyecto.FechaActualizacion,
                };

                _dbContext.Proyectos.Add(dbProyecto);
                await _dbContext.SaveChangesAsync();

                if(dbProyecto.Id != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProyecto.Id;
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

                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(e => e.Id == id);

                 
                if (dbProyecto != null)
                {

                    dbProyecto.Nombre = proyecto.Nombre;
                    dbProyecto.Descripcion = proyecto.Descripcion;
                    dbProyecto.FechaInicio = proyecto.FechaInicio;
                    dbProyecto.FechaFin = proyecto.FechaFin;
                    dbProyecto.Prioridad = proyecto.Prioridad;
                    dbProyecto.Estado = proyecto.Estado;
                    dbProyecto.PresupuestoEstimado = proyecto.PresupuestoEstimado;
                    dbProyecto.RecursosAsignados = proyecto.RecursosAsignados;
                     
                    dbProyecto.FechaActualizacion = DateTime.Now;


                    _dbContext.Proyectos.Update(dbProyecto);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProyecto.Id;

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
            var responseApi = new ResponseAPI<int>();


            try
            {

                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(e => e.Id == id);


                if (dbProyecto != null)
                {

                    _dbContext.Proyectos.Remove(dbProyecto);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                     

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
