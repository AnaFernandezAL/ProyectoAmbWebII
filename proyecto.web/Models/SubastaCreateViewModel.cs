using proyecto.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace proyecto.web.Models
{
    public class SubastaCreateViewModel
    {
        // DTO de aplicación para la orden
        public SubastaDTO subasta { get; set; } = new();

        [Display(Name = "Nombre del cliente")]
        public string? NombreCliente { get; set; }

        public List<UsuarioDTO> Usuarios { get; set; } = new();
    }
}
