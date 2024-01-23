namespace SistemaVentaBlazor.Client.Servicios.Contrato
{
    public interface IHorarioActividadService
    {
        Task<ResponseDTO<List<HorarioActividadDTO>>> Lista();
        Task<ResponseDTO<HorarioActividadDTO>> Crear(HorarioActividadDTO entidad);
        Task<bool> Editar(HorarioActividadDTO entidad);
        Task<bool> Eliminar(int id);
        Task<ResponseDTO<HorarioActividadDTO>> Obtener(int id);
    }
}
