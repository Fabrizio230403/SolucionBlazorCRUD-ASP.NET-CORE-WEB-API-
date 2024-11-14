using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface ITareaService
    {
        Task<List<TareaDTO>> Lista();
        Task<TareaDTO> Buscar(int id);
        Task<int> Guardar(TareaDTO tarea);
        Task<int> Editar(TareaDTO tarea);
        Task<bool> Eliminar(int id);
        Task<List<ProyectoDTO>> ObtenerProyectos();
        Task<List<UsuarioDTO>> ObtenerUsuarios();
    }
}
