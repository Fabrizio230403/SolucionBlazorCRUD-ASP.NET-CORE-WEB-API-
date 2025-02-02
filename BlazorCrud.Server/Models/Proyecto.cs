﻿using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Proyecto
{
    public int ProyectoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public string? Prioridad { get; set; }

    public string? Estado { get; set; }

    public int? GerenteId { get; set; }

    public decimal? PorcentajeCompleto { get; set; }

    public virtual Usuario? Gerente { get; set; }

    public virtual ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
