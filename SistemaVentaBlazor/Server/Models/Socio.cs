using System;
using System.Collections.Generic;

namespace SistemaVentaBlazor.Server.Models;

public partial class Socio
{
    public int IdSocio { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? Clave { get; set; }

    public string? Correo { get; set; }

    public DateTime? FechaInicioMembresia { get; set; }

    public bool? EstadoPago { get; set; }

    public string? Restricciones { get; set; }

    public string? Mutualista { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; } = new List<Asistencia>();

    public virtual ICollection<PagoMensual> PagoMensuals { get; } = new List<PagoMensual>();
}
