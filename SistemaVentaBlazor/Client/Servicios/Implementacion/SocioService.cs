using SistemaVentaBlazor.Client.Utilidades;
using System.Net.Http;
using System.Net.Http.Json;

namespace SistemaVentaBlazor.Client.Servicios.Implementacion
{
    public class SocioService : ISocioService
    {
        private readonly HttpClient _http;
        
        public SocioService(HttpClient http)
        {
            _http = http;
        }
        
        public async Task<ResponseDTO<SocioDTO>> Crear(SocioDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/socio/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<SocioDTO>>();
            return response!;
        }

        public async Task<bool> Editar(SocioDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/socio/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<SocioDTO>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/socio/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<SocioDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<SocioDTO>>>("api/socio/Lista");
            return result!;
        }

        public async Task<ResponseDTO<SocioDTO>> Obtener(int id)
        {
            return await _http.GetFromJsonAsync<ResponseDTO<SocioDTO>>($"api/socio/Obtener/{id}");
        }
    }
}
