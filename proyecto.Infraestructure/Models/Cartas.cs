using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models;

public partial class Cartas
{
    public int CartaId { get; set; }

    public string NombreCarta { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Condicion { get; set; } = null!;

    public int EstadoCartaId { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int VendedorId { get; set; }

    public string? Edicion { get; set; }

    public string? Rareza { get; set; }

    public virtual EstadosCarta EstadoCarta { get; set; } = null!;

    public virtual ICollection<ImagenesCarta> ImagenesCarta { get; set; } = new List<ImagenesCarta>();

    public virtual ICollection<Subastas> Subastas { get; set; } = new List<Subastas>();

    public virtual Usuarios Vendedor { get; set; } = null!;
    public virtual ICollection<CartaCategoria> CartaCategoria { get; set; } = new List<CartaCategoria>();

    public virtual ICollection<Categorias> Categoria { get; set; } = new List<Categorias>();
}
