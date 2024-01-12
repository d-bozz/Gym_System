using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentaBlazor.Shared
{
    public class SocioDTO
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
    }
}
