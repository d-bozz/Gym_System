namespace SistemaVentaBlazor.Shared
{
    public class AsistenciaDTO
    {
        public int IdAsistencia { get; set; }

        public int? IdSocio { get; set; }

        public int? IdHorario { get; set; }

        public DateTime? FechaAsistencia { get; set; }
    }
}
