using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models;

public partial class Subastas
{
    public int SubastaId { get; set; }

    public int CartaId { get; set; }

    public int VendedorId { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaCierre { get; set; }

    public decimal PrecioBase { get; set; }

    public decimal IncrementoMinimo { get; set; }

    public int EstadoSubastaId { get; set; }

    public decimal? MontoActual { get; set; }

    public virtual Cartas Carta { get; set; } = null!;

    public virtual EstadosSubasta EstadoSubasta { get; set; } = null!;

    public virtual Ganadores? Ganadores { get; set; }

    public virtual Pagos? Pagos { get; set; }

    public virtual ICollection<Pujas> Pujas { get; set; } = new List<Pujas>();

    public virtual Usuarios Vendedor { get; set; } = null!;
}
