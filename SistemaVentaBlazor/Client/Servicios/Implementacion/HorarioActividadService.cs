using System.Net.Http.Json;

namespace SistemaVentaBlazor.Client.Servicios.Implementacion
{
    public class HorarioActividadService : IHorarioActividadService
    {
        #region Properties and Fields
        private readonly HttpClient _http;

        public HorarioActividadService(HttpClient http)
        {
            _http = http;
        }
        #endregion

        public async Task<ResponseDTO<HorarioActividadDTO>> Crear(HorarioActividadDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/horarioactividad/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<HorarioActividadDTO>>();
            return response!;
        }

        public async Task<bool> Editar(HorarioActividadDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/horarioactividad/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<HorarioActividadDTO>>();
            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/horarioactividad/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<HorarioActividadDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<HorarioActividadDTO>>>("api/horarioactividad/Lista");
            return result!;
        }

        public async Task<ResponseDTO<HorarioActividadDTO>> Obtener(int id)
        {
            return await _http.GetFromJsonAsync<ResponseDTO<HorarioActividadDTO>>($"api/socio/horarioactividad/{id}");
        }
    }
}
