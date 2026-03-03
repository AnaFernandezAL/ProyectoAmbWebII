using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace proyecto.Application.DTOs
{
    public record UsuarioDTO
    {
        [DisplayName("Identificador Usuario")]
        public int UsuarioId { get; set; }

        [DisplayName("Nombre Completo")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string NombreCompleto { get; set; } = string.Empty;

        [DisplayName("Correo Electrónico")]
        [Required(ErrorMessage = "{0} es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Correo { get; set; } = string.Empty;

        [DisplayName("Teléfono")]
        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        public string? Telefono { get; set; }

        [DisplayName("Rol")]
        [Required(ErrorMessage = "{0} es requerido")]
        public RolDTO Rol { get; set; } = new();

        [DisplayName("Estado")]
        [Required(ErrorMessage = "{0} es requerido")]
        public EstadoUsuarioDTO EstadoUsuario { get; set; } = new();

        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

        [DisplayName("Cantidad de Subastas Creadas")]
        public int CantidadSubastasCreadas { get; set; }

        [DisplayName("Cantidad de Pujas Realizadas")]
        public int CantidadPujasRealizadas { get; set; }
    }
}