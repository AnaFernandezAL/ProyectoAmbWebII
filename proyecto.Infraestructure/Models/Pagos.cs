using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models;

public partial class Pagos
{
    public int PagoId { get; set; }

    public int SubastaId { get; set; }

    public int CompradorId { get; set; }

    public int EstadoPagoId { get; set; }

    public DateTime? FechaPago { get; set; }

    public virtual Usuarios Comprador { get; set; } = null!;

    public virtual EstadosPago EstadoPago { get; set; } = null!;

    public virtual Subastas Subasta { get; set; } = null!;
}
