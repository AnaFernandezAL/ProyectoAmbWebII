using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.DTOs
{
    public record ImagenCartaDTO
    {
        [DisplayName("Identificador Imagen")]
        public int ImagenId { get; set; }

        [DisplayName("Identificador Carta")]
        public int CartaId { get; set; }

        [DisplayName("URL de la Imagen")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string UrlImagen { get; set; } = string.Empty;

        [DisplayName("Es Principal")]
        public bool EsPrincipal { get; set; }
    }
}
