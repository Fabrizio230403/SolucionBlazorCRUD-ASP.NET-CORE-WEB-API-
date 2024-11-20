using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IRecursoService
    {
        Task<List<RecursoDTO>> Lista();
        Task<RecursoDTO> Buscar(int id);
        Task<int> Guardar(RecursoDTO recurso);
        Task<int> Editar(RecursoDTO recurso);
        Task<bool> Eliminar(int id);
        Task<List<RecursoDTO>> ObtenerRecursosPorUsuario(int usuarioId);
    }
}
