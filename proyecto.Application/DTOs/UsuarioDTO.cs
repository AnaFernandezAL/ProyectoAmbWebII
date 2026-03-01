using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.DTOs
{
    public record UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty; 
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }

        // Campos calculados con LINQ en servicios
        public int CantidadSubastasCreadas { get; set; } 
        public int CantidadPujasRealizadas { get; set; }

    }
}
