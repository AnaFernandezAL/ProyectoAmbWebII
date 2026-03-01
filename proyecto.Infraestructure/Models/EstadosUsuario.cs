using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models;

public partial class EstadosUsuario
{
    public int EstadoUsuarioId { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
}
