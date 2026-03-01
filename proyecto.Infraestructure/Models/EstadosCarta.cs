using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models;

public partial class EstadosCarta
{
    public int EstadoCartaId { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Cartas> Cartas { get; set; } = new List<Cartas>();
}
