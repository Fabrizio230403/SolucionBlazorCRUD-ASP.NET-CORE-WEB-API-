using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IRolPermisoService
    {
        Task<List<RolPermisoDTO>> Lista();
        Task<List<String>> ListaPermisos(int usuarioId);
    }
}