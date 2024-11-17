using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IRolService
    {
        Task<List<RolDTO>> Lista();
    }
}
