using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models;

public partial class Pujas
{
    public int PujaId { get; set; }

    public int SubastaId { get; set; }

    public int CompradorId { get; set; }

    public decimal MontoOfertado { get; set; }

    public DateTime? FechaHora { get; set; }

    public bool Notificado { get; set; }

    public virtual Usuarios Comprador { get; set; } = null!;

    public virtual Subastas Subasta { get; set; } = null!;
}
