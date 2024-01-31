using System.ComponentModel.DataAnnotations;

namespace SistemaVentaBlazor.Shared
{
    public class SocioDTO
    {
        public int IdSocio { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El campo Teléfono es obligatorio.")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Por favor, ingrese una dirección de correo electrónico válida.")]
        public string? Correo { get; set; }

        public DateTime? FechaInicioMembresia { get; set; }

        public bool Activo { get; set; }

        public string? Restricciones { get; set; }

        public string? Mutualista { get; set; }

        public string? Direccion { get; set; }

        public string? TelefonoContacto { get; set; }
    }
}
