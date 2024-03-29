﻿using System;
using System.Collections.Generic;

namespace SistemaVentaBlazor.Server.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreApellidos { get; set; }

    public string? Correo { get; set; }

    public int? IdRol { get; set; }

    public string? Clave { get; set; }

    public bool? EsActivo { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }
    public string? Cedula { get; set; }

    public string? Foto { get; set; }

    public string? Horario { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
