using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? RolId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? TokenRecuperacion { get; set; }

    public DateTime? FechaExpiracionToken { get; set; }

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();

    public virtual ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();

    public virtual Role? Rol { get; set; }

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
