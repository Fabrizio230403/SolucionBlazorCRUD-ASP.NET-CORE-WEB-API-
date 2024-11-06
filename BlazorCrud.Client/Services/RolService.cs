using BlazorCrud.Shared;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorCrud.Client.Services
{
    public class RolService:IRolService
    {
        private readonly HttpClient _http;

        public RolService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<RolDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<RolDTO>>>("api/Rol/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        
  
    }
}
 
