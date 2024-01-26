using System;
using System.Collections.Generic;

namespace SistemaVentaBlazor.Server.Models;

public partial class HorarioActividad
{
    public int IdHorario { get; set; }

    public string? DiaSemana { get; set; }

    public TimeSpan? HoraInicio { get; set; }

    public TimeSpan? HoraFin { get; set; }

    public string? Actividad { get; set; }

    public string? Encargado { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; } = new List<Asistencia>();
}
