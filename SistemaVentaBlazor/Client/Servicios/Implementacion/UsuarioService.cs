using SistemaVentaBlazor.Client.Utilidades;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.EMMA;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SistemaVentaBlazor.Client.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;
        public UsuarioService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ResponseDTO<UsuarioDTO>> Crear(UsuarioDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/usuario/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<UsuarioDTO>>();
            return response!;
        }

        public async Task<bool> Editar(UsuarioDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/usuario/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<UsuarioDTO>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/usuario/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<UsuarioDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<UsuarioDTO>>>("api/usuario/Lista");
            return result!;
        }

        public async Task<ResponseDTO<UsuarioDTO>> Obtener(int id)
        {
            return await _http.GetFromJsonAsync<ResponseDTO<UsuarioDTO>>($"api/usuario/Obtener/{id}");
        }

        public async Task<ResponseDTO<UsuarioDTO>> Validar(UsuarioLogin request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync($"api/usuario/Validar", request);

                response.EnsureSuccessStatusCode();
                
                var result = await response.Content.ReadFromJsonAsync<ResponseDTO<UsuarioDTO>>();

                if (result != null && result.status)
                {
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.token);
                }

                return result;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }
    }
}
