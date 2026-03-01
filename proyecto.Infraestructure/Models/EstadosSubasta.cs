using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models;

public partial class EstadosSubasta
{
    public int EstadoSubastaId { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Subastas> Subastas { get; set; } = new List<Subastas>();
}
