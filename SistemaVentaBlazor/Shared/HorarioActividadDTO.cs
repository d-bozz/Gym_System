using System.ComponentModel.DataAnnotations;

namespace SistemaVentaBlazor.Shared
{
    public class HorarioActividadDTO
    {
        public int IdHorario { get; set; }

        [Required(ErrorMessage = "El día de la semana es requerido.")]
        public string? DiaSemana { get; set; }

        [Required(ErrorMessage = "La hora de inicio es requerida.")]
        public TimeSpan? HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de fin es requerida.")]
        public TimeSpan? HoraFin { get; set; }

        [Required(ErrorMessage = "La actividad es requerida.")]
        public string? Actividad { get; set; }

        [Required(ErrorMessage = "El encargado es requerido.")]
        public string? Encargado { get; set; }
    }
}
