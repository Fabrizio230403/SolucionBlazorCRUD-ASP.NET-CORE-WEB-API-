using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Role
{
    public int RolId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
}
