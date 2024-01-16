using System.ComponentModel.DataAnnotations;

namespace SistemaVentaBlazor.Shared
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo Nombre y Apellidos es obligatorio.")]
        public string? NombreApellidos { get; set; }

        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Por favor, ingrese una dirección de correo electrónico válida.")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "El campo IdRol es obligatorio.")]
        public int IdRol { get; set; }

        public string? rolDescripcion { get; set; }

        [Required(ErrorMessage = "El campo Clave es obligatorio.")]
        public string? Clave { get; set; }

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public string? Horario { get; set; }
    }
}
