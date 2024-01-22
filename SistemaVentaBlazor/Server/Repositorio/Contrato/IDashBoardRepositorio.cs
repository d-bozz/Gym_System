namespace SistemaVentaBlazor.Server.Repositorio.Contrato
{
    public interface IDashBoardRepositorio
    {
        #region Estadisticas de Socios
        Task<int> TotalSocios();
        Task<int> TotalSociosActivos();
        Task<int> NuevosSocios();
        Task<int> PagosPendientes();
        #endregion

        #region Estadisticas por Ventas
        Task<int> TotalVentasUltimaSemana();
        Task<string> TotalIngresosUltimaSemana();
        Task<int> TotalProductos();
        Task<Dictionary<string, int>> VentasUltimaSemana();
        #endregion
    }
}
