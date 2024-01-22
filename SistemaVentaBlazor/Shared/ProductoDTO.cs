using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentaBlazor.Shared
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? IdCategoria { get; set; }
        public string? DescripcionCategoria { get; set; }
        public int? Stock { get; set; }
        public decimal? Precio { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? Marca { get; set; }
        public string? Peso { get; set; }
        public bool? EsActivo { get; set; }
        public string? Foto { get; set; }
    }
}
