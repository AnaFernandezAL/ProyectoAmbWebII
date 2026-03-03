using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace proyecto.Application.DTOs
{
    public record EstadoSubastaDTO
    {
        [DisplayName("Identificador Estado Subasta")]
        public int EstadoSubastaId { get; set; }

        [DisplayName("Nombre Estado")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string NombreEstado { get; set; } = string.Empty;
    }
}

