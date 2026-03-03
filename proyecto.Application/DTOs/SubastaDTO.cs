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

        [DisplayName("Fecha de Inicio")]
        [Required(ErrorMessage = "{0} es requerido")]
        public DateTime FechaInicio { get; set; }

        [DisplayName("Fecha de Cierre")]
        [Required(ErrorMessage = "{0} es requerido")]
        public DateTime FechaCierre { get; set; }

        [DisplayName("Precio Base")]
        [Required(ErrorMessage = "{0} es requerido")]
        [DataType(DataType.Currency)]
        public decimal PrecioBase { get; set; }

        [DisplayName("Incremento Mínimo")]
        [Required(ErrorMessage = "{0} es requerido")]
        [DataType(DataType.Currency)]
        public decimal IncrementoMinimo { get; set; }

        [DisplayName("Monto Actual")]
        [DataType(DataType.Currency)]
        public decimal? MontoActual { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "{0} es requerido")]
        public EstadoSubastaDTO EstadoSubasta { get; set; } = new();

        [DisplayName("Carta Subastada")]
        [Required(ErrorMessage = "{0} es requerido")]
        public CartaDTO Carta { get; set; } = new();

        [DisplayName("Vendedor")]
        [Required(ErrorMessage = "{0} es requerido")]
        public UsuarioDTO Vendedor { get; set; } = new();

        // Campo calculado
        [DisplayName("Cantidad de Pujas")]
        public int CantidadPujas { get; set; }
    }
}