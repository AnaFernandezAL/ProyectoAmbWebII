using proyecto.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace proyecto.web.ViewModels
{
    public class SubastaCreateViewModel
    {
        // DTO de aplicación para la orden
        public SubastaCreateDTO Subasta { get; set; } = new();

    }
}
