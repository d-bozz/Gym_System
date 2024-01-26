using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentaBlazor.Shared
{
    public class HorarioActividadDTO
    {
        public int IdHorario { get; set; }

        public string? DiaSemana { get; set; }

        public TimeSpan? HoraInicio { get; set; }

        public TimeSpan? HoraFin { get; set; }

        public string? Actividad { get; set; }

        public string? Encargado { get; set; }
    }
}
