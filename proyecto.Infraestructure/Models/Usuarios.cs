using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models;

public partial class Usuarios
{
    public int UsuarioId { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? TokenRecuperacion { get; set; }

    public int EstadoUsuarioId { get; set; }

    public int RolId { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool Notificado { get; set; }

    public virtual ICollection<Cartas> Cartas { get; set; } = new List<Cartas>();

    public virtual EstadosUsuario EstadoUsuario { get; set; } = null!;

    public virtual ICollection<Ganadores> Ganadores { get; set; } = new List<Ganadores>();

    public virtual ICollection<Pagos> Pagos { get; set; } = new List<Pagos>();

    public virtual ICollection<Pujas> Pujas { get; set; } = new List<Pujas>();

    public virtual Roles Rol { get; set; } = null!;

    public virtual ICollection<Subastas> Subastas { get; set; } = new List<Subastas>();
}
