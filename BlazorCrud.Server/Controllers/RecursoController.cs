using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursoController : ControllerBase
    {

        private readonly SistemaConsultoriaContext _dbContext;

        public RecursoController(SistemaConsultoriaContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<RecursoDTO>>();
            var listaRecursoDTO = new List<RecursoDTO>();

            try
            {
                foreach (var item in await _dbContext.Recursos.Include(p => p.Proyecto).Include(u => u.Usuario).ToListAsync())
                {
                    listaRecursoDTO.Add(new RecursoDTO
                    {
                        RecursoId = item.RecursoId,
                        ProyectoId = item.ProyectoId,
                        Tipo = item.Tipo,
                        Nombre = item.Nombre,
                        Costo = item.Costo,
                        TiempoAsignado = item.TiempoAsignado,
                        UsuarioId = item.UsuarioId,
                        Proyecto = new ProyectoDTO
                        {
                            ProyectoID = item.Proyecto!.ProyectoID,
                            Nombre = item.Proyecto.Nombre,
                            Descripcion = item.Proyecto.Descripcion,
                            FechaInicio = item.Proyecto.FechaInicio,
                            FechaFin = item.Proyecto.FechaFin,
                            Prioridad = item.Proyecto.Prioridad,
                            Estado = item.Proyecto.Estado,
                            GerenteID = item.Proyecto.GerenteID,
                            PorcentajeCompleto = item.Proyecto.PorcentajeCompleto,
                        },
                        Usuario = new UsuarioDTO
                        {
                            UsuarioId = item.Usuario!.UsuarioId,
                            Nombre = item.Usuario.Nombre,
                            Email = item.Usuario.Email,
                            PasswordHash = item.Usuario.PasswordHash,
                            RolId = item.Usuario.RolId,
                            FechaCreacion = item.Usuario.FechaCreacion,
                        }


                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaRecursoDTO;

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
            var responseApi = new ResponseAPI<RecursoDTO>();
            var RecursoDTO = new RecursoDTO();

            try
            {
                var dbRecurso = await _dbContext.Recursos.FirstOrDefaultAsync(x => x.RecursoId == id);

                if (dbRecurso != null)
                {
                    RecursoDTO.RecursoId = dbRecurso.RecursoId;
                    RecursoDTO.ProyectoId = dbRecurso.ProyectoId;
                    RecursoDTO.Nombre = dbRecurso.Nombre;
                    RecursoDTO.Tipo = dbRecurso.Tipo;
                    RecursoDTO.Costo = dbRecurso.Costo;
                    RecursoDTO.TiempoAsignado = dbRecurso.TiempoAsignado;
                    RecursoDTO.UsuarioId = dbRecurso.UsuarioId;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = RecursoDTO;
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
        public async Task<IActionResult> Guardar(RecursoDTO recurso)
        {
            var responseApi = new ResponseAPI<int>();


            try
            {
                var dbRecurso = new Recurso
                {
                    ProyectoId = recurso.ProyectoId,
                    Nombre = recurso.Nombre,
                    Tipo = recurso.Tipo,
                    Costo = recurso.Costo,
                    TiempoAsignado = recurso.TiempoAsignado,
                    UsuarioId = recurso.UsuarioId,
                };

                _dbContext.Recursos.Add(dbRecurso);
                await _dbContext.SaveChangesAsync();

                if (dbRecurso.RecursoId != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbRecurso.RecursoId;
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
        public async Task<IActionResult> Editar(RecursoDTO recurso, int id)
        {
            var responseApi = new ResponseAPI<int>();


            try
            {

                var dbRecurso = await _dbContext.Recursos.FirstOrDefaultAsync(e => e.RecursoId == id);


                if (dbRecurso != null)
                {

                    dbRecurso.ProyectoId = recurso.ProyectoId;
                    dbRecurso.Nombre = recurso.Nombre;
                    dbRecurso.Tipo = recurso.Tipo;
                    dbRecurso.Costo = recurso.Costo;
                    dbRecurso.TiempoAsignado = recurso.TiempoAsignado;
                    dbRecurso.UsuarioId = recurso.UsuarioId;

                    _dbContext.Recursos.Update(dbRecurso);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbRecurso.RecursoId;

                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Recurso no encontrado";
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

                var dbRecurso = await _dbContext.Recursos.FirstOrDefaultAsync(e => e.RecursoId == id);


                if (dbRecurso != null)
                {

                    _dbContext.Recursos.Remove(dbRecurso);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;


                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Recurso no encontrado";
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