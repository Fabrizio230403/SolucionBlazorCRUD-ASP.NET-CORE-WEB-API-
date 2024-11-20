using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Permiso
{
    public int PermisoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public ICollection<RolesPermiso> RolPermisos { get; set; } = new List<RolesPermiso>();

}
