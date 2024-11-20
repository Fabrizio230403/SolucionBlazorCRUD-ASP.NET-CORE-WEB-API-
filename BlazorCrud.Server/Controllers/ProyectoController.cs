using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
                foreach(var item in await _dbContext.Proyectos.Include(u => u.Gerente).ToListAsync())
                {
                    listaProyectoDTO.Add(new ProyectoDTO
                    {
                        ProyectoId = item.ProyectoId,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        FechaInicio = item.FechaInicio,
                        FechaFin = item.FechaFin,
                        Prioridad = item.Prioridad,
                        Estado = item.Estado,
                        GerenteId = item.GerenteId,
                        PorcentajeCompleto = item.PorcentajeCompleto,
                        Gerente = new UsuarioDTO
                        {
                            UsuarioId = item.Gerente!.UsuarioId,
                            Nombre = item.Gerente.Nombre,
                            Email = item.Gerente.Email,
                            PasswordHash = item.Gerente.PasswordHash,
                            RolId = item.Gerente.RolId,
                            FechaCreacion = item.Gerente.FechaCreacion,
                        }
                         
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
                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(x => x.ProyectoId == id);

                if(dbProyecto != null)
                {
                    ProyectoDTO.ProyectoId = dbProyecto.ProyectoId;
                    ProyectoDTO.Nombre = dbProyecto.Nombre;
                    ProyectoDTO.Descripcion = dbProyecto.Descripcion;
                    ProyectoDTO.FechaInicio = dbProyecto.FechaInicio;
                    ProyectoDTO.FechaFin = dbProyecto.FechaFin;
                    ProyectoDTO.Prioridad = dbProyecto.Prioridad;
                    ProyectoDTO.Estado = dbProyecto.Estado;
                    ProyectoDTO.GerenteId = dbProyecto.GerenteId;
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
                    GerenteId = proyecto.GerenteId,
                    PorcentajeCompleto = proyecto.PorcentajeCompleto,
               
                };

                _dbContext.Proyectos.Add(dbProyecto);
                await _dbContext.SaveChangesAsync();

                if(dbProyecto.ProyectoId != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProyecto.ProyectoId;
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

                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(e => e.ProyectoId == id);

                 
                if (dbProyecto != null)
                {

                    dbProyecto.Nombre = proyecto.Nombre;
                    dbProyecto.Descripcion = proyecto.Descripcion;
                    dbProyecto.FechaInicio = proyecto.FechaInicio;
                    dbProyecto.FechaFin = proyecto.FechaFin;
                    dbProyecto.Prioridad = proyecto.Prioridad;
                    dbProyecto.Estado = proyecto.Estado;
                    dbProyecto.GerenteId = proyecto.GerenteId;
                    dbProyecto.PorcentajeCompleto = proyecto.PorcentajeCompleto;
                     

                    _dbContext.Proyectos.Update(dbProyecto);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProyecto.ProyectoId;

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

                var dbProyecto = await _dbContext.Proyectos.FirstOrDefaultAsync(e => e.ProyectoId == id);


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


        [HttpGet]
        [Route("Lista/{usuarioId}")]
        public async Task<IActionResult> ListaPorUsuario(int usuarioId)
        {
            var responseApi = new ResponseAPI<List<ProyectoDTO>>();
            var listaProyectoDTO = new List<ProyectoDTO>();

            try
            {
                 
                foreach (var item in await _dbContext.Proyectos
                    .Where(t => t.GerenteId == usuarioId) // Filtrar proyectos por el usuario
                    .Include(u => u.Gerente)
                    .ToListAsync())
                {
                    listaProyectoDTO.Add(new ProyectoDTO
                    {
                        ProyectoId = item.ProyectoId,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        FechaInicio = item.FechaInicio,
                        FechaFin = item.FechaFin,
                        Prioridad = item.Prioridad,
                        Estado = item.Estado,
                        GerenteId = item.GerenteId,
                        PorcentajeCompleto = item.PorcentajeCompleto,
                        Gerente = new UsuarioDTO
                        {
                            UsuarioId = item.Gerente!.UsuarioId,
                            Nombre = item.Gerente.Nombre,
                            Email = item.Gerente.Email,
                            PasswordHash = item.Gerente.PasswordHash,
                            RolId = item.Gerente.RolId,
                            FechaCreacion = item.Gerente.FechaCreacion
                        }
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
        [Route("EnviarProyectos/{usuarioId}")]
        public async Task<IActionResult> EnviarProyectosPorUsuario(int usuarioId)
        {
            var responseApi = new ResponseAPI<string>();
            var listaProyectoDTO = new List<ProyectoDTO>();

            try
            {
                // Obtener los proyectos del usuario
                foreach (var item in await _dbContext.Proyectos
                    .Where(t => t.GerenteId == usuarioId)
                    .Include(u => u.Gerente)
                    .ToListAsync())
                {
                    listaProyectoDTO.Add(new ProyectoDTO
                    {
                        ProyectoId = item.ProyectoId,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        FechaInicio = item.FechaInicio,
                        FechaFin = item.FechaFin,
                        Prioridad = item.Prioridad,
                        Estado = item.Estado,
                        GerenteId = item.GerenteId,
                        PorcentajeCompleto = item.PorcentajeCompleto,
                        Gerente = new UsuarioDTO
                        {
                            UsuarioId = item.Gerente!.UsuarioId,
                            Nombre = item.Gerente.Nombre,
                            Email = item.Gerente.Email
                        }
                    });
                }

                // Construir el cuerpo del correo
                var gerente = listaProyectoDTO.FirstOrDefault()?.Gerente;
                if (gerente == null)
                    throw new Exception("No se encontró información del gerente");

                var cuerpo = "<h1>Proyectos Asignados</h1><ul>";
                foreach (var proyecto in listaProyectoDTO)
                {
                    cuerpo += $"<li><b>{proyecto.Nombre}</b>: {proyecto.Descripcion} (Estado: {proyecto.Estado})</li>";
                }
                cuerpo += "</ul>";

                // Enviar el correo
                var emailService = HttpContext.RequestServices.GetService<EmailNotiService>();
                await emailService!.EnviarCorreoAsync(
                    destinatario: gerente.Email,
                    asunto: "Proyectos Asignados",
                    cuerpo: cuerpo
                );

                responseApi.EsCorrecto = true;
                responseApi.Valor = "Correo enviado exitosamente.";
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
