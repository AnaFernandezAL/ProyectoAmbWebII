using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.DTOs
{
    public record CategoriaDTO
    {
        [DisplayName("Identificador Categoría")]
        public int CategoriaId { get; set; }

        [DisplayName("Nombre de la Categoría")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string NombreCategoria { get; set; } = string.Empty;
    }
}
