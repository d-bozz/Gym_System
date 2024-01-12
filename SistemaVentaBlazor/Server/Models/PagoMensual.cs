using System;
using System.Collections.Generic;

namespace SistemaVentaBlazor.Server.Models;

public partial class PagoMensual
{
    public int IdPago { get; set; }

    public int? IdSocio { get; set; }

    public int? Mes { get; set; }

    public int? Anio { get; set; }

    public DateTime? FechaPago { get; set; }

    public decimal? Monto { get; set; }

    public virtual Socio? IdSocioNavigation { get; set; }
}
