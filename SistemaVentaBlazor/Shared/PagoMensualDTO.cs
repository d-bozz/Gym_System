﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentaBlazor.Shared
{
    public class PagoMensualDTO
    {
        public int IdPago { get; set; }

        public int? IdSocio { get; set; }

        public int? Mes { get; set; }

        public int? Anio { get; set; }

        public DateTime? FechaPago { get; set; }

        public decimal? Monto { get; set; }
    }
}