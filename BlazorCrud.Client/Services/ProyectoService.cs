using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class ProyectoService: IProyectoService
    {

        private readonly HttpClient _http;

        public ProyectoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProyectoDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<ProyectoDTO>>>("api/Proyecto/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<ProyectoDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<ProyectoDTO>>($"api/Proyecto/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> Guardar(ProyectoDTO proyecto)
        {
            var result = await _http.PostAsJsonAsync("api/Proyecto/Guardar", proyecto);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Editar(ProyectoDTO proyecto)
        {
            var result = await _http.PutAsJsonAsync($"api/Proyecto/Editar/{proyecto.ProyectoID}", proyecto);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Proyecto/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
        
    }
}
