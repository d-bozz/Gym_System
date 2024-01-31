namespace SistemaVentaBlazor.Shared
{
    public class PagoMensualDTO
    {
        public int IdPago { get; set; }
        public int? IdSocio { get; set; }
        public string? nombreSocio { get; set; }
        public string? apellidoSocio { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? Monto { get; set; }
    }
}
