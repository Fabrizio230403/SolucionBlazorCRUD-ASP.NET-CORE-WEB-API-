namespace BlazorCrud.Server.Models;

public partial class Tarea
{
    public int TareaID { get; set; }
    public int ProyectoID { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateOnly? FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
    public string? Estado { get; set; }
    public int UsuarioAsignadoID { get; set; }
    public virtual Proyecto? Proyecto { get; set; }
    public virtual Usuario? UsuarioAsignado { get; set; }
}
