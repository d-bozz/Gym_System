using SistemaVentaBlazor.Client.Utilidades;

namespace SistemaVentaBlazor.Client.Servicios.Contrato
{
    public interface ISocioService
    {
        Task<ResponseDTO<List<SocioDTO>>> Lista();
        Task<ResponseDTO<SocioDTO>> Crear(SocioDTO entidad);
        Task<bool> Editar(SocioDTO entidad);
        Task<bool> Eliminar(int id);
        Task<ResponseDTO<SocioDTO>> Obtener(int id);
    }
}
