using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.DTOs
{
    public record EstadoCartaDTO
    {
        [DisplayName("Identificador Estado Carta")]
        public int EstadoCartaId { get; set; }

        [DisplayName("Nombre del Estado")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string NombreEstado { get; set; } = string.Empty;
    }
}

