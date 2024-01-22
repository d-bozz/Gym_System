using System.Net.Http.Json;

namespace SistemaVentaBlazor.Client.Servicios.Implementacion
{
    public class PagoMensualService : IPagoMensualService
    {
        #region Properties and Fields
        private readonly HttpClient _http;

        public PagoMensualService(HttpClient http)
        {
            _http = http;
        }
        #endregion

        public async Task<ResponseDTO<PagoMensualDTO>> Crear(PagoMensualDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/pagomensual/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<PagoMensualDTO>>();
            return response!;
        }

        public async Task<bool> Editar(PagoMensualDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/pagomensual/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<PagoMensualDTO>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/pagomensual/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<PagoMensualDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<PagoMensualDTO>>>("api/pagomensual/Lista");
            return result!;
        }

        public async Task<ResponseDTO<List<PagoMensualDTO>>> ListaPorSocio(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<PagoMensualDTO>>>($"api/pagomensual/ListaPorSocio/{id}");
            return result!;
        }

        public async Task<ResponseDTO<PagoMensualDTO>> Obtener(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<PagoMensualDTO>>($"api/pagomensual/Obtener/{id}");
            return result!;
        }
    }
}
