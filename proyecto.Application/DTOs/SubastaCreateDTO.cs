using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.DTOs
{
    public class SubastaCreateDTO
    {
        public int CartaId { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaCierre { get; set; }

        [Required]
        public decimal PrecioBase { get; set; }

        [Required]
        public decimal IncrementoMinimo { get; set; }
    }
}
