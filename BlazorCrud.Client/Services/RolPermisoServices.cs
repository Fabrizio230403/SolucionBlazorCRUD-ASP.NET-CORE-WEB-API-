using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class RolPermisoServices:IRolPermisoService
    {

        private readonly HttpClient _http;

        public RolPermisoServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<RolPermisoDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<RolPermisoDTO>>>("api/RolPermiso/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<List<String>> ListaPermisos(int usuarioId)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<String>>>($"api/RolPermiso/ListaPermisos/{usuarioId}");

            Console.WriteLine($"Resultado de la API: {result?.EsCorrecto}, {result?.Mensaje}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

    }
}
