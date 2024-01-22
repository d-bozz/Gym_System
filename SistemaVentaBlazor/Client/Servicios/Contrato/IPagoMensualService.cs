namespace SistemaVentaBlazor.Client.Servicios.Contrato
{
    public interface IPagoMensualService
    {
        Task<ResponseDTO<List<PagoMensualDTO>>> Lista();
        Task<ResponseDTO<List<PagoMensualDTO>>> ListaPorSocio(int id);
        Task<ResponseDTO<PagoMensualDTO>> Crear(PagoMensualDTO entidad);
        Task<bool> Editar(PagoMensualDTO entidad);
        Task<bool> Eliminar(int id);
        Task<ResponseDTO<PagoMensualDTO>> Obtener(int id);
    }
}
