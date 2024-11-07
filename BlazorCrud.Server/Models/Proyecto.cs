namespace BlazorCrud.Server.Models;

public partial class Proyecto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
    public string? Prioridad { get; set; }
    public string? Estado { get; set; }
    public decimal? PresupuestoEstimado { get; set; }
    public string? RecursosAsignados { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
}
