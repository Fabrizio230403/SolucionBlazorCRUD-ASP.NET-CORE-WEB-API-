using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class RecursoService:IRecursoService
    {
        private readonly HttpClient _http;

        public RecursoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<RecursoDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<RecursoDTO>>>("api/Recurso/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<RecursoDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<RecursoDTO>>($"api/Recurso/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> Guardar(RecursoDTO recurso)
        {
            var result = await _http.PostAsJsonAsync("api/Recurso/Guardar", recurso);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Editar(RecursoDTO recurso)
        {
            var result = await _http.PutAsJsonAsync($"api/Recurso/Editar/{recurso.RecursoId}", recurso);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Recurso/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<List<RecursoDTO>> ObtenerRecursosPorUsuario(int usuarioId)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<RecursoDTO>>>($"api/Recurso/Lista/{usuarioId}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
    }
}
