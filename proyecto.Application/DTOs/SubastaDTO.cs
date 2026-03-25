using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace proyecto.Application.DTOs
{
    public record SubastaDTO
    {
        [DisplayName("Identificador Subasta")]
        public int SubastaId { get; set; }

        public int CartaId { get; set; }

        public int VendedorId { get; set; }
        public int EstadoSubastaId { get; set; }

        [DisplayName("Fecha de Inicio")]
        [Required(ErrorMessage = "{0} es requerido")]
        public DateTime FechaInicio { get; set; }

        [DisplayName("Fecha de Cierre")]
        [Required(ErrorMessage = "{0} es requerido")]
        public DateTime FechaCierre { get; set; }

        [DisplayName("Precio Base")]
        [Required(ErrorMessage = "El precio base es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio base debe ser mayor a 0")]
        [DataType(DataType.Currency)]
        public decimal PrecioBase { get; set; }

        [DisplayName("Incremento Mínimo")]
        [Required(ErrorMessage = "El incremento mínimo es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        [DataType(DataType.Currency)]
        public decimal IncrementoMinimo { get; set; }

        [DisplayName("Monto Actual")]
        [DataType(DataType.Currency)]
        public decimal? MontoActual { get; set; }

        [DisplayName("Estado")]
        public EstadoSubastaDTO EstadoSubasta { get; set; } = new();

        [DisplayName("Carta Subastada")]
        public CartaDTO Carta { get; set; } = new();

        [DisplayName("Vendedor")]
        public UsuarioDTO Vendedor { get; set; } = new();

        // Campo calculado
        [DisplayName("Cantidad de Pujas")]
        public int CantidadPujas { get; set; }
    }
}