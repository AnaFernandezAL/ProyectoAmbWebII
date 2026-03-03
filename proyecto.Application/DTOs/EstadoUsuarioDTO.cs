using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.DTOs
{
    public record EstadoUsuarioDTO
    {
        [DisplayName("Identificador Estado")]
        public int EstadoUsuarioId { get; set; }

        [DisplayName("Nombre Estado")]
        public string Nombre { get; set; } = string.Empty;
    }
}

