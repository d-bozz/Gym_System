using System;
using System.Collections.Generic;

namespace SistemaVentaBlazor.Server.Models;

public partial class Socio
{
    public int IdSocio { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public DateTime? FechaInicioMembresia { get; set; }

    public bool? Activo { get; set; }

    public string? Restricciones { get; set; }

    public string? Mutualista { get; set; }

    public string? Direccion { get; set; }
    public string? Cedula { get; set; }

    public string? TelefonoContacto { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; } = new List<Asistencia>();

    public virtual ICollection<PagoMensual> PagoMensuales { get; } = new List<PagoMensual>();
}
