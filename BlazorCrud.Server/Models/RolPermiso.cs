namespace BlazorCrud.Server.Models
{
    public partial class RolPermiso
    {

        public int RolId { get; set; }
        public int PermisoId { get; set; }

        public Role Rol { get; set; } = null!;
        public Permiso Permiso { get; set; } = null!;

    }
}
