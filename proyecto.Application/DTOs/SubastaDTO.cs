using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.DTOs
{
    public record SubastaDTO
    {
        public int SubastaId { get; set; }
        public string NombreCarta { get; set; } = string.Empty;
        public string ImagenPrincipal { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal IncrementoMinimo { get; set; }
        public int CantidadPujas { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}

