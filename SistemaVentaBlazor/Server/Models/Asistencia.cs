using System;
using System.Collections.Generic;

namespace SistemaVentaBlazor.Server.Models;

public partial class Asistencia
{
    public int IdAsistencia { get; set; }

    public int? IdSocio { get; set; }

    public int? IdHorario { get; set; }

    public DateTime? FechaAsistencia { get; set; }

    public virtual HorarioActividad? IdHorarioNavigation { get; set; }

    public virtual Socio? IdSocioNavigation { get; set; }
}
