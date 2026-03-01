using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models;

public partial class EstadosPago
{
    public int EstadoPagoId { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Pagos> Pagos { get; set; } = new List<Pagos>();
}
