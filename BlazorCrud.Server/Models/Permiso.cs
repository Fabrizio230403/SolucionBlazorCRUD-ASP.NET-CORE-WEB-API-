﻿namespace BlazorCrud.Server.Models;

public partial class Permiso
{

    public int PermisoID { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }

}

