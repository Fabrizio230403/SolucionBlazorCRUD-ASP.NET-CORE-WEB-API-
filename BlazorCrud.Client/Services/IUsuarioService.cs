using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> Lista();
        Task<UsuarioDTO> Buscar(int id);
        Task<int> Guardar(UsuarioDTO usuario);
        Task<int> Editar(UsuarioDTO usuario);
        Task<bool> Eliminar(int id);
    }
}
