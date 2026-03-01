using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models;

public partial class Ganadores
{
    public int GanadorId { get; set; }

    public int SubastaId { get; set; }

    public int UsuarioId { get; set; }

    public decimal MontoFinal { get; set; }

    public DateTime FechaCierre { get; set; }

    public virtual Subastas Subasta { get; set; } = null!;

    public virtual Usuarios Usuario { get; set; } = null!;
}
