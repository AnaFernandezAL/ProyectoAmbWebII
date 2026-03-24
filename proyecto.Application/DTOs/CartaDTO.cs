using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace proyecto.Application.DTOs
{
    public record CartaDTO
    {
        // Identificador
        public int CartaId { get; set; }

        // Datos principales
        [Required(ErrorMessage = "{0} es requerido")]
        public string NombreCarta { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} es requerido")]
        [MinLength(20, ErrorMessage = "La descripción debe tener al menos 20 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} es requerido")]
        public string Condicion { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} es requerido")]
        public string Edicion { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} es requerido")]
        public string Rareza { get; set; } = string.Empty;

        // Estado y vendedor
        public int EstadoCartaId { get; set; }
        public int VendedorId { get; set; }
        public DateTime? FechaRegistro { get; set; }

        // Para creación
        [Required(ErrorMessage = "Debe seleccionar al menos una categoría")]
        public List<int> CategoriasSeleccionadas { get; set; } = new();

        [Required(ErrorMessage = "Debe subir al menos una imagen")]
        public List<IFormFile> ImagenesCarta { get; set; } = new();
        public int ImagenPrincipalIndex { get; set; }

        // Para visualización
        public ICollection<CartaCategoriaDTO> CartaCategoria { get; set; } = new List<CartaCategoriaDTO>();
        public ICollection<ImagenCartaDTO> ImagenesCartaNavigation { get; set; } = new List<ImagenCartaDTO>();
        public ICollection<SubastaDTO> Subastas { get; set; } = new List<SubastaDTO>();

        public EstadoCartaDTO? EstadoCarta { get; set; }
        public UsuarioDTO? Vendedor { get; set; }
    }
}
