using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace proyecto.Application.DTOs
{
    public record RolDTO
    {
        [DisplayName("Identificador Rol")]
        public int RolId { get; set; }

        [DisplayName("Nombre Rol")]
        public string Nombre { get; set; } = string.Empty;
    }
}

