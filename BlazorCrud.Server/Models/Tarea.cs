using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Tarea
{
    public int TareaId { get; set; }

    public int? ProyectoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public string? Estado { get; set; }

    public int? UsuarioAsignadoId { get; set; }

    public virtual Proyecto? Proyecto { get; set; }

    public virtual Usuario? UsuarioAsignado { get; set; }
}
