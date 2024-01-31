namespace SistemaVentaBlazor.Shared
{
    public class DashBoardDTO
    {
        #region Estadisticas de Socios
        public int? TotalSocios { get; set; }
        public int? TotalSociosActivos { get; set; }
        public int? NuevosSocios { get; set; }
        public int? PagosPendientes { get; set; }
        #endregion

        #region Estadisticas por Ventas
        public string? TotalIngresos { get; set; }
        public int? TotalVentas { get; set; }
        public int? TotalProductos { get; set; }
        public List<VentaSemanaDTO>? VentasUltimaSemana { get; set; }
        #endregion
    }
}
