using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ClosedXML.Excel;


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


        [HttpGet]
        [Route("ReportePDF")]
        public async Task<IActionResult> GenerarReportePDF()
        {
            try
            {
                var tareas = await _dbContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(u => u.UsuarioAsignado)
                    .ToListAsync();

                using var memoryStream = new MemoryStream();
                var document = new Document();
                var writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Título del documento
                var titulo = new Paragraph("Reporte de Tareas")
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(titulo);

                // Tabla
                var table = new PdfPTable(6) { WidthPercentage = 100 };
                table.AddCell("Tarea ID");
                table.AddCell("Nombre");
                table.AddCell("Descripción");
                table.AddCell("Estado");
                table.AddCell("Fecha Inicio");
                table.AddCell("Fecha Fin");

                foreach (var tarea in tareas)
                {
                    table.AddCell(tarea.TareaId.ToString());
                    table.AddCell(tarea.Nombre);
                    table.AddCell(tarea.Descripcion);
                    table.AddCell(tarea.Estado);
                    
                }

                document.Add(table);
                document.Close();

                return File(memoryStream.ToArray(), "application/pdf", "ReporteTareas.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [HttpGet]
        [Route("ReporteExcel")]
        public async Task<IActionResult> GenerarReporteExcel()
        {
            try
            {
                var tareas = await _dbContext.Tareas
                    .Include(p => p.Proyecto)
                    .Include(u => u.UsuarioAsignado)
                    .ToListAsync();

                // Crear un libro de trabajo (workbook) de ClosedXML
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Reporte de Tareas");

                // Cabeceras
                worksheet.Cell(1, 1).Value = "Tarea ID";
                worksheet.Cell(1, 2).Value = "Nombre";
                worksheet.Cell(1, 3).Value = "Descripción";
                worksheet.Cell(1, 4).Value = "Estado";
                worksheet.Cell(1, 5).Value = "Fecha Inicio";
                worksheet.Cell(1, 6).Value = "Fecha Fin";

                // Datos
                int row = 2;
                foreach (var tarea in tareas)
                {
                    worksheet.Cell(row, 1).Value = tarea.TareaId;
                    worksheet.Cell(row, 2).Value = tarea.Nombre;
                    worksheet.Cell(row, 3).Value = tarea.Descripcion;
                    worksheet.Cell(row, 4).Value = tarea.Estado;
                 
                    row++;
                }

                // Ajustar el tamaño de las columnas
                worksheet.Columns().AdjustToContents();

                // Guardar el archivo en memoria
                using var memoryStream = new MemoryStream();
                workbook.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Retornar el archivo Excel como respuesta
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteTareas.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
