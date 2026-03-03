using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models
{
    public partial class CartaCategoria
    {
        public int CartaId { get; set; }
        public int CategoriaId { get; set; }

        public virtual Cartas Carta { get; set; } = null!;
        public virtual Categorias Categoria { get; set; } = null!;
    }
}
