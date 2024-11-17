using BlazorCrud.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class TareaService : ITareaService
    {
        private readonly HttpClient _http;

        public TareaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TareaDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<TareaDTO>>>("api/Tarea/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<TareaDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<TareaDTO>>($"api/Tarea/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> Guardar(TareaDTO tarea)
        {
            var result = await _http.PostAsJsonAsync("api/Tarea/Guardar", tarea);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Editar(TareaDTO tarea)
        {
            var result = await _http.PutAsJsonAsync($"api/Tarea/Editar/{tarea.TareaID}", tarea);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Tarea/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<List<ProyectoDTO>> ObtenerProyectos()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<ResponseAPI<List<ProyectoDTO>>>("api/Proyecto/Lista");

                if (result != null && result.EsCorrecto && result.Valor != null)
                {
                    return result.Valor;
                }
                else
                {
                    throw new Exception(result?.Mensaje ?? "Error desconocido al obtener proyectos");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener proyectos: {ex.Message}");
            }
        }


        public async Task<List<UsuarioDTO>> ObtenerUsuarios()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<ResponseAPI<List<UsuarioDTO>>>("api/Empleado/Lista");

                if (result != null && result.EsCorrecto && result.Valor != null)
                {
                    return result.Valor;
                }
                else
                {
                    throw new Exception(result?.Mensaje ?? "Error desconocido al obtener usuarios");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener usuarios: {ex.Message}");
            }
        }



    }
}
