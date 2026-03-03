using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace proyecto.Application.DTOs
{
    public record CartaCategoriaDTO
    {
        [DisplayName("Identificador Carta")]
        public int CartaId { get; set; }

        [DisplayName("Identificador Categoría")]
        public int CategoriaId { get; set; }

        [DisplayName("Categoría")]
        public CategoriaDTO Categoria { get; set; } = new();
    }
}

