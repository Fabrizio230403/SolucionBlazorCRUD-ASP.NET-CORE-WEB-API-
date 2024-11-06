using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Recurso
{
    public int RecursoId { get; set; }

    public int? ProyectoId { get; set; }

    public string? Tipo { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal? Costo { get; set; }

    public decimal? TiempoAsignado { get; set; }

    public int? UsuarioId { get; set; }

    public virtual Proyecto? Proyecto { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
