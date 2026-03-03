using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.DTOs
{
    public record CartaDTO
    {
        [DisplayName("Identificador Carta")]
        public int CartaId { get; set; }

        [DisplayName("Nombre de la Carta")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string NombreCarta { get; set; } = string.Empty;

        [DisplayName("Descripción")]
        public string? Descripcion { get; set; }

        [DisplayName("Condición")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string Condicion { get; set; } = string.Empty;

        [DisplayName("Estado")]
        [Required(ErrorMessage = "{0} es requerido")]
        public EstadoCartaDTO EstadoCarta { get; set; } = new();

        [DisplayName("Fecha de Registro")]
        public DateTime? FechaRegistro { get; set; }

        [DisplayName("Edición")]
        public string? Edicion { get; set; }

        [DisplayName("Rareza")]
        public string? Rareza { get; set; }

        [DisplayName("Propietario")]
        [Required(ErrorMessage = "{0} es requerido")]
        public UsuarioDTO Vendedor { get; set; } = new();

        [DisplayName("Categorías asignadas")]
        public ICollection<CartaCategoriaDTO> CartaCategoria { get; set; } = new List<CartaCategoriaDTO>();

        [DisplayName("Imágenes")]
        public ICollection<ImagenCartaDTO> ImagenesCarta { get; set; } = new List<ImagenCartaDTO>();

        [DisplayName("Subastas")]
        public ICollection<SubastaDTO> Subastas { get; set; } = new List<SubastaDTO>();
    }
}