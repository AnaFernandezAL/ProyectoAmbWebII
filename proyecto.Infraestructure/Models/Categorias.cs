using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models;

public partial class Categorias
{
    public int CategoriaId { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public virtual ICollection<Cartas> Carta { get; set; } = new List<Cartas>();
    public virtual ICollection<CartaCategoria> CartaCategoria { get; set; } = new List<CartaCategoria>();

}
