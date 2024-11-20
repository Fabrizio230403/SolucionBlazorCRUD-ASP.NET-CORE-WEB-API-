using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IProyectoService
    {
        Task<List<ProyectoDTO>> Lista();
        Task<ProyectoDTO> Buscar(int id);
        Task<int> Guardar(ProyectoDTO proyecto);
        Task<int> Editar(ProyectoDTO proyecto);
        Task<bool> Eliminar(int id);
        Task<List<ProyectoDTO>> ObtenerProyectosAsignadosPorUsuario(int usuarioId);
    }
}
