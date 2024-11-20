using BlazorCrud.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class TareaService:ITareaService
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
            var result = await _http.PutAsJsonAsync($"api/Tarea/Editar/{tarea.TareaId}", tarea);
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

        public async Task<List<TareaDTO>> ObtenerTareasPorUsuario(int usuarioId)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<TareaDTO>>>($"api/Tarea/Lista/{usuarioId}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<byte[]> GenerarReportePDF()
        {
            var result = await _http.GetAsync("api/Tarea/ReportePDF");

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsByteArrayAsync();
            }
            else
            {
                throw new Exception("Error al generar el reporte PDF");
            }
        }

        public async Task<byte[]> GenerarReporteExcel()
        {
            var result = await _http.GetAsync("api/Tarea/ReporteExcel");

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsByteArrayAsync();
            }
            else
            {
                throw new Exception("Error al generar el reporte Excel");
            }
        }

    }
}
